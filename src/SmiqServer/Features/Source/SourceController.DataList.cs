using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SmiqServer.Features.Source.Builders.DigitalModulation;

namespace SmiqServer.Features.Source
{
    public partial class SourceController
    {
        [HttpGet("DigitalModulation/DataLists")]
        public Task<string[]> GetAll(DataList.GetAll query) => query.ExecuteAsync(_instrument);

        [HttpGet("DigitalModulation/DataLists/{Name}")]
        public Task<byte[]> Get(DataList.GetData query) => query.ExecuteAsync(_instrument);

        [HttpPut("DigitalModulation/DataLists/{Name}")]
        public Task Set(DataList.SetData command) => command.ExecuteAsync(_instrument);

        [HttpDelete("DigitalModulation/DataLists/{Name}")]
        public Task Delete(DataList.Delete command) => command.ExecuteAsync(_instrument);
    }
}
