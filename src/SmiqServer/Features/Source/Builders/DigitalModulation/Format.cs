using System;

namespace SmiqServer.Features.Source.Builders.DigitalModulation
{
    public class Format
    {
        public class Get : QueryBuilder<FormatType>
        {
            public override string BuildPayload() => "SOURce:DM:FORMat?";

            public override FormatType ParsePayload(string response) => Mappings.ParseFormatType(response);
        }

        public class Set : CommandBuilder
        {
            public FormatType Value { get; set; }

            public Set()
            {
            }

            public Set(FormatType value) => Value = value;

            public override string BuildPayload() => string.Format("SOURce:DM:FORMat {0}", Value.ToSerialString());
        }
    }
}
