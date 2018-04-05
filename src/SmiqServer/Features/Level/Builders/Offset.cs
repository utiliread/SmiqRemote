namespace SmiqServer.Features.Level.Builders
{
    public class Offset
    {
        public class Get : QueryBuilder<decimal>
        {
            public override string BuildPayload() => "SOURce:POWer:LEVel:OFFSet?";

            public override decimal ParsePayload(string response) => decimal.Parse(response);
        }

        public class Set : CommandBuilder
        {
            public decimal Power { get; set; }

            public Set()
            {
            }

            public Set(decimal power) => Power = power;

            public override string BuildPayload() => string.Format("SOURce:POWer:LEVel:OFFSet {0}dB", Power);
        }
    }
}
