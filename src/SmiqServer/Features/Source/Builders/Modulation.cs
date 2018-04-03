namespace SmiqServer.Features.Source.Builders
{
    /// <summary>
    /// §3.5.14.14
    /// </summary>
    public class Modulation
    {
        public class Get : QueryBuilder<bool>
        {
            public override string BuildPayload() => "SOURce:MODulation:STATe?";

            public override bool ParsePayload(string response) => response == "1";
        }

        public class Set : CommandBuilder
        {
            public bool Value { get; set; }

            public Set()
            {
            }

            public Set(bool value) => Value = value;

            public override string BuildPayload() => string.Format("SOURce:MODulation:STATe {0}", Value ? "ON" : "OFF");
        }
    }
}
