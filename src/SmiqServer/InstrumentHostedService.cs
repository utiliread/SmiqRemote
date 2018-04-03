using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RJCP.IO.Ports;
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmiqServer
{
    public class Command
    {
        public byte[] Request { get; set; }
        public bool ExpectResponse { get; set; }
        public CancellationToken CancellationToken { get; set; }
        public TaskCompletionSource<byte[]> TaskCompletionSource { get; set; }
    }

    public class InstrumentHostedService : HostedService, IInstrument
    {
        private readonly IOptions<InstrumentOptions> _options;
        private readonly ILogger<InstrumentHostedService> _logger;
        private readonly ConcurrentQueue<Command> _queue = new ConcurrentQueue<Command>();
        private readonly SemaphoreSlim _wait = new SemaphoreSlim(0);

        public InstrumentHostedService(IOptions<InstrumentOptions> options, ILogger<InstrumentHostedService> logger)
        {
            _options = options;
            _logger = logger;
        }

        public Task CommandAsync(byte[] request, CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<byte[]>(TaskCreationOptions.RunContinuationsAsynchronously);

            _queue.Enqueue(new Command()
            {
                Request = request,
                CancellationToken = cancellationToken,
                TaskCompletionSource = tcs
            });

            _wait.Release();

            return tcs.Task;
        }

        public Task<byte[]> QueryAsync(byte[] request, CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<byte[]>(TaskCreationOptions.RunContinuationsAsynchronously);

            _queue.Enqueue(new Command()
            {
                Request = request,
                CancellationToken = cancellationToken,
                ExpectResponse = true,
                TaskCompletionSource = tcs
            });

            _wait.Release();

            return tcs.Task;
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            var options = _options.Value;

            using (var port = new SerialPortStream(options.Port, options.Baud))
            {
                port.Open();

                // Clear Status
                var cls = Encoding.ASCII.GetBytes("*CLS\n");
                await port.WriteAsync(cls, 0, cls.Length);

                // §3.5.10
                var formatPacked = Encoding.ASCII.GetBytes("FORMat PACKed\n");
                await port.WriteAsync(formatPacked, 0, formatPacked.Length);

                // §3.5.17
                var lterEoi = Encoding.ASCII.GetBytes("SYSTem:COMMunicate:GPIB:LTERminator EOI\n");
                await port.WriteAsync(lterEoi, 0, lterEoi.Length);

                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        await _wait.WaitAsync(cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                    }

                    if (_queue.TryDequeue(out var item))
                    {
                        if (item.CancellationToken.IsCancellationRequested)
                        {
                            continue;
                        }

                        _logger.LogDebug("Sending {Request}", Encoding.ASCII.GetString(item.Request));

                        await port.WriteAsync(item.Request, 0, item.Request.Length, cancellationToken);
                        await port.FlushAsync(cancellationToken);

                        if (item.ExpectResponse)
                        {
                            var buffer = ArrayPool<byte>.Shared.Rent(1024);
                            var offset = 0;

                            try
                            {
                                var length = -1;

                                while (length == -1 || offset < length)
                                {
                                    var increased = await port.ReadAsync(buffer, offset, buffer.Length - offset, cancellationToken);

                                    var relativeIndex = buffer.AsSpan().Slice(offset, increased).IndexOfAny((byte)'#', (byte)'\n');
                                    var index = offset + relativeIndex;

                                    offset += increased;

                                    if (index >= 0)
                                    {
                                        if (buffer[index] == '#')
                                        {
                                            length = BlockData.DecodeLength(buffer.AsSpan().Slice(0, offset));
                                        }
                                        else if (index > 0)
                                        {
                                            length = index;
                                        }
                                    }
                                }

                                var response = buffer.AsSpan().Slice(0, length).ToArray();

                                _logger.LogDebug("Received {ResponseBytes} bytes: {Response}", response.Length, Encoding.ASCII.GetString(response));

                                item.TaskCompletionSource.SetResult(response);
                            }
                            finally
                            {
                                ArrayPool<byte>.Shared.Return(buffer);
                            }
                        }
                        else
                        {
                            item.TaskCompletionSource.SetResult(null);
                        }
                    }
                }

                port.Close();
            }
        }
    }
}
