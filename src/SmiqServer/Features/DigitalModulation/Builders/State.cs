namespace SmiqServer.Features.DigitalModulation.Builders
{
    public class State
    {
        public class Get : QueryBuilder<bool>
        {
            public override string BuildPayload() => "SOURce:DM:STATe?";

            public override bool ParsePayload(string response) => response == "1";
        }

        public class Set : CommandBuilder
        {
            public bool Value { get; set; }

            public Set()
            {
            }

            public Set(bool value) => Value = value;

            public override string BuildPayload() => string.Format("SOURce:DM:STATe {0}", Value ? "ON" : "OFF");
        }
    }
}
