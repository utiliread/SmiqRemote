using Microsoft.Extensions.Options;

namespace SmiqServer
{
    public class InstrumentOptions : IOptions<InstrumentOptions>
    {
        public string Port { get; set; }
        public int Baud { get; set; } = 9600;

        InstrumentOptions IOptions<InstrumentOptions>.Value => this;
    }
}
