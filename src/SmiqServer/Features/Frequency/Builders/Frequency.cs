namespace SmiqServer.Features.Frequency.Builders
{
    public class Frequency
    {
        public class Get : QueryBuilder<decimal>
        {
            public override string BuildPayload() => "SOURce:FREQuency?";

            public override decimal ParsePayload(string response) => decimal.Parse(response);
        }

        public class Set : CommandBuilder
        {
            public decimal Value { get; set; }

            public Set()
            {
            }

            public Set(decimal value) => Value = value;

            public override string BuildPayload() => string.Format("SOURce:FREQuency {0}Hz", Value);
        }
    }
}
