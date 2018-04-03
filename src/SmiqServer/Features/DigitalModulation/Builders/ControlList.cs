using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmiqServer.Features.DigitalModulation.Builders
{
    public class ControlList
    {
        public class Get : QueryBuilder<string>
        {
            public override string BuildPayload() => "SOURce:DM:CLISt:SELect?";

            public override string ParsePayload(string response) => response;
        }

        public class GetAll : QueryBuilder<string[]>
        {
            public override string BuildPayload() => "SOURce:DM:CLISt:CATalog?";

            public override string[] ParsePayload(string response) => response.Split(',');
        }

        public class Set : CommandBuilder
        {
            public string Name { get; set; }

            public Set()
            {
            }

            public Set(string name) => Name = name;

            public override string BuildPayload() => string.Format("SOURce:DM:CLISt:SELect '{0}'", Name);
        }

        public class GetData : IQueryBuilder<ControlSignalDto[]>
        {
            public string Name { get; set; }

            public GetData()
            {
            }

            public GetData(string name) => Name = name;

            public byte[] BuildQuery()
            {
                var query = string.Format("SOURce:DM:CLISt:SELect '{0}'\nSOURce:DM:CLISt:DATA?\n", Name);

                return Encoding.ASCII.GetBytes(query);
            }

            public ControlSignalDto[] ParseResponse(byte[] response)
            {
                var data = BlockData.Decode(response);
                var signals = new List<ControlSignalDto>(data.Length / 4);

                for (var offset = 0; offset < data.Length; offset += 4)
                {
                    var signal = ControlSignalDto.Decode(data.AsSpan().Slice(offset, 4));
                    signals.Add(signal);
                }

                return signals.ToArray();
            }
        }

        public class SetData : ICommandBuilder
        {
            public string Name { get; set; }

            [FromBody]
            public ControlSignalDto[] Value { get; set; }

            public SetData()
            {
            }

            public SetData(string name, ControlSignalDto[] value)
            {
                Name = name;
                Value = value;
            }

            public byte[] BuildCommand()
            {
                var header = string.Format("SOURce:DM:CLISt:SELect '{0}'\nSOURce:DM:CLISt:DATA ", Name);

                var value = Value.SelectMany(x => x.Encode()).ToArray();

                return Encoding.ASCII.GetBytes(header).Concat(BlockData.Encode(value)).ToArray();
            }
        }

        public class Delete : CommandBuilder
        {
            public string Name { get; set; }

            public Delete()
            {
            }

            public Delete(string name) => Name = name;

            public override string BuildPayload() => string.Format("SOURce:DM:CLISt:DELete '{0}'", Name);
        }
    }
}
