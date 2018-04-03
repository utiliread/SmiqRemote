using Microsoft.AspNetCore.Mvc;
using SmiqServer.Features.DigitalModulation.Builders;
using System.Threading.Tasks;

namespace SmiqServer.Features.DigitalModulation
{
    public partial class DigitalModulationController
    {
        [HttpGet("DataLists")]
        public Task<string[]> GetAll(DataList.GetAll query) => query.ExecuteAsync(_instrument);

        [HttpGet("DataLists/{Name}")]
        public Task<byte[]> Get(DataList.GetData query) => query.ExecuteAsync(_instrument);

        [HttpPut("DataLists/{Name}")]
        public Task Set(DataList.SetData command) => command.ExecuteAsync(_instrument);

        [HttpDelete("DataLists/{Name}")]
        public Task Delete(DataList.Delete command) => command.ExecuteAsync(_instrument);
    }
}
