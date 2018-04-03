using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SmiqServer.Features.Common.Builders;

namespace SmiqServer.Features.Common
{
    /// <summary>
    /// §3.5.2 Common Commands
    /// </summary>
    [Route("/[controller]")]
    public class CommonController : ControllerBase
    {
        private readonly IInstrument _instrument;

        public CommonController(IInstrument instrument)
        {
            _instrument = instrument;
        }

        [HttpGet("[action]")]
        public Task<string> Identification(Identification query) => query.ExecuteAsync(_instrument);

        [HttpPost("[action]")]
        public Task Reset(Reset command) => command.ExecuteAsync(_instrument);

        [HttpPost("[action]")]
        public Task Trigger(Trigger command) => command.ExecuteAsync(_instrument);
    }
}
