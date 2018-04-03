namespace SmiqServer.Features.Source.Builders
{
    /// <summary>
    /// §3.5.14.21
    /// </summary>
    public class Power
    {
        public class Get : QueryBuilder<decimal>
        {
            public override string BuildPayload() => "SOURce:POWer?";

            public override decimal ParsePayload(string response) => decimal.Parse(response);
        }

        public class Set : CommandBuilder
        {
            public decimal Power { get; set; }

            public Set()
            {
            }

            public Set(decimal power) => Power = power;

            public override string BuildPayload() => string.Format("SOURce:POWer {0}dBm", Power);
        }
    }
}
