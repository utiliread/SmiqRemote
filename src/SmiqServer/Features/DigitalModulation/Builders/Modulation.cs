namespace SmiqServer.Features.DigitalModulation.Builders
{
    public class Modulation
    {
        public class Get : QueryBuilder<ModulationType>
        {
            public override string BuildPayload() => "SOURce:DM:FORMat?";

            public override ModulationType ParsePayload(string response) => Mappings.ParseFormatType(response);
        }

        public class Set : CommandBuilder
        {
            public ModulationType Value { get; set; }

            public Set()
            {
            }

            public Set(ModulationType value) => Value = value;

            public override string BuildPayload() => string.Format("SOURce:DM:FORMat {0}", Value.ToSerialString());
        }
    }
}
