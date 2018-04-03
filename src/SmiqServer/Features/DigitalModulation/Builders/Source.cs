namespace SmiqServer.Features.DigitalModulation.Builders
{
    public class Source
    {
        public class Get : QueryBuilder<SourceType>
        {
            public override string BuildPayload() => "SOURce:DM:SOURce?";

            public override SourceType ParsePayload(string response) => Mappings.ParseSourceType(response);
        }

        public class Set : CommandBuilder
        {
            public SourceType Value { get; set; }

            public Set()
            {
            }

            public Set(SourceType value) => Value = value;

            public override string BuildPayload() => string.Format("SOURce:DM:SOURce {0}", Value.ToSerialString());
        }
    }
}
