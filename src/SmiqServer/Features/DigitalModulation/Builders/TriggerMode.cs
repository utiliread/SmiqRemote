namespace SmiqServer.Features.DigitalModulation.Builders
{
    public class TriggerMode
    {
        public class Get : QueryBuilder<TriggerModeType>
        {
            public override string BuildPayload() => "SOURce:DM:SEQuence?";

            public override TriggerModeType ParsePayload(string response) => Mappings.ParseSequenceType(response);
        }

        public class Set : CommandBuilder
        {
            public TriggerModeType Value { get; set; }

            public Set()
            {
            }

            public Set(TriggerModeType value) => Value = value;

            public override string BuildPayload() => string.Format("SOURce:DM:SEQuence {0}", Value.ToSerialString());
        }
    }
}
