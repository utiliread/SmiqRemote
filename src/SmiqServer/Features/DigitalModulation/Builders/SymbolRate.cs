namespace SmiqServer.Features.DigitalModulation.Builders
{
    public class SymbolRate
    {
        public class Get : QueryBuilder<decimal>
        {
            public override string BuildPayload() => "SOURce:DM:SRATe?";

            public override decimal ParsePayload(string response) => decimal.Parse(response);
        }

        public class Set : CommandBuilder
        {
            public decimal Value { get; set; }

            public Set()
            {
            }

            public Set(decimal value) => Value = value;

            public override string BuildPayload() => string.Format("SOURce:DM:SRATe {0}Hz", Value);
        }
    }
}
