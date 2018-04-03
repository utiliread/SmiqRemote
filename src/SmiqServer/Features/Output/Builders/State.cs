namespace SmiqServer.Features.Output.Builders
{
    /// <summary>
    /// §3.5.12
    /// </summary>
    public class State
    {
        public class Get : QueryBuilder<bool>
        {
            public override string BuildPayload() => "OUTPut:STATe?";

            public override bool ParsePayload(string response) => response == "1";
        }

        public class Set : CommandBuilder
        {
            public bool Value { get; set; }

            public Set()
            {
            }

            public Set(bool value) => Value = value;

            public override string BuildPayload() => string.Format("OUTPut:STATe {0}", Value ? "ON" : "OFF");
        }
    }
}
