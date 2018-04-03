namespace SmiqServer.Features.Source.Builders.DigitalModulation
{
    public class FskDeviation
    {
        public class Get : QueryBuilder<decimal>
        {
            public override string BuildPayload() => "SOURce:DM:FSK:DEViation?";

            public override decimal ParsePayload(string response) => decimal.Parse(response);
        }

        public class Set : CommandBuilder
        {
            public decimal Value { get; set; }

            public Set()
            {
            }

            public Set(decimal value) => Value = value;

            public override string BuildPayload() => string.Format("SOURce:DM:FSK:DEViation {0}Hz", Value);
        }
    }
}
