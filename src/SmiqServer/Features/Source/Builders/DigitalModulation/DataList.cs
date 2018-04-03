using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;

namespace SmiqServer.Features.Source.Builders.DigitalModulation
{
    public class DataList
    {
        public class Get : QueryBuilder<string>
        {
            public override string BuildPayload() => "SOURce:DM:DLISt:SELect?";

            public override string ParsePayload(string response) => response;
        }

        public class GetAll : QueryBuilder<string[]>
        {
            public override string BuildPayload() => "SOURce:DM:DLISt:CATalog?";

            public override string[] ParsePayload(string response) => response.Split(',');
        }

        public class Set : CommandBuilder
        {
            public string Name { get; set; }

            public Set()
            {
            }

            public Set(string name) => Name = name;

            public override string BuildPayload() => string.Format("SOURce:DM:DLISt:SELect '{0}'", Name);
        }

        public class GetData : IQueryBuilder<byte[]>
        {
            public string Name { get; set; }

            public GetData()
            {
            }

            public GetData(string name) => Name = name;

            public byte[] BuildQuery()
            {
                var query = string.Format("SOURce:DM:DLISt:SELect '{0}'\nSOURce:DM:DLISt:DATA?\n", Name);

                return Encoding.ASCII.GetBytes(query);
            }

            public byte[] ParseResponse(byte[] response) => BlockData.Decode(response);
        }

        public class SetData : ICommandBuilder
        {
            public string Name { get; set; }

            [FromBody]
            public byte[] Value { get; set; }

            public SetData()
            {
            }

            public SetData(string name, byte[] value)
            {
                Name = name;
                Value = value;
            }

            public byte[] BuildCommand()
            {
                var header = string.Format("SOURce:DM:DLISt:SELect '{0}'\nSOURce:DM:DLISt:DATA ", Name);

                return Encoding.ASCII.GetBytes(header).Concat(BlockData.Encode(Value)).ToArray();
            }
        }

        public class Delete : CommandBuilder
        {
            public string Name { get; set; }

            public Delete()
            {
            }

            public Delete(string name) => Name = name;

            public override string BuildPayload() => string.Format("SOURce:DM:DLISt:DELete '{0}'", Name);
        }
    }
}
