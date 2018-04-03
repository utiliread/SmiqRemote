using System.Threading;
using System.Threading.Tasks;

namespace SmiqServer
{
    public interface IInstrument
    {
        Task CommandAsync(byte[] request, CancellationToken cancellationToken = default);
        Task<byte[]> QueryAsync(byte[] request, CancellationToken cancellationToken = default);
    }
}
