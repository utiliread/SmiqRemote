using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SmiqServer.Features.Source.Builders.DigitalModulation;

namespace SmiqServer.Features.Source
{
    public partial class SourceController
    {
        [HttpGet("DigitalModulation/ControlLists")]
        public Task<string[]> GetAll(ControlList.GetAll query) => query.ExecuteAsync(_instrument);

        [HttpGet("DigitalModulation/ControlLists/{Name}")]
        public Task<ControlSignalDto[]> Get(ControlList.GetData query) => query.ExecuteAsync(_instrument);

        [HttpPut("DigitalModulation/ControlLists/{Name}")]
        public Task Set(ControlList.SetData command) => command.ExecuteAsync(_instrument);

        [HttpDelete("DigitalModulation/ControlLists/{Name}")]
        public Task Delete(ControlList.Delete command) => command.ExecuteAsync(_instrument);
    }
}
