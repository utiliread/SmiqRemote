namespace SmiqServer.Features.DigitalModulation.Builders
{
    public class Filter
    {
        public class Get : QueryBuilder<FilterType>
        {
            public override string BuildPayload() => "SOURce:DM:FILTer:TYPE?";

            public override FilterType ParsePayload(string response) => Mappings.ParseFilterType(response);
        }

        public class Set : CommandBuilder
        {
            public FilterType Value { get; set; }

            public Set()
            {
            }

            public Set(FilterType value) => Value = value;

            public override string BuildPayload() => string.Format("SOURce:DM:FILTer:TYPE {0}", Value.ToSerialString());
        }
    }
}
