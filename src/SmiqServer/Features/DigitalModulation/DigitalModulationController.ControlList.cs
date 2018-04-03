using Microsoft.AspNetCore.Mvc;
using SmiqServer.Features.DigitalModulation.Builders;
using System.Threading.Tasks;

namespace SmiqServer.Features.DigitalModulation
{
    public partial class DigitalModulationController
    {
        [HttpGet("ControlLists")]
        public Task<string[]> GetAll(ControlList.GetAll query) => query.ExecuteAsync(_instrument);

        [HttpGet("ControlLists/{Name}")]
        public Task<ControlSignalDto[]> Get(ControlList.GetData query) => query.ExecuteAsync(_instrument);

        [HttpPut("ControlLists/{Name}")]
        public Task Set(ControlList.SetData command) => command.ExecuteAsync(_instrument);

        [HttpDelete("ControlLists/{Name}")]
        public Task Delete(ControlList.Delete command) => command.ExecuteAsync(_instrument);
    }
}
