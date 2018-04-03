using System;

namespace SmiqServer.Features.Source.Builders
{
    public class Sequence
    {
        public class Get : QueryBuilder<SequenceType>
        {
            public override string BuildPayload() => "SOURce:DM:SEQuence?";

            public override SequenceType ParsePayload(string response) => Mappings.ParseSequenceType(response);
        }

        public class Set : CommandBuilder
        {
            public SequenceType Value { get; set; }

            public Set()
            {
            }

            public Set(SequenceType value) => Value = value;

            public override string BuildPayload() => string.Format("SOURce:DM:SEQuence {0}", Value.ToSerialString());
        }
    }
}
