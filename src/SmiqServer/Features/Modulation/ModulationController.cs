using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SmiqServer.Features.Modulation.Builders;

namespace SmiqServer.Features.Modulation
{
    /// <summary>
    /// §3.5.14 Source System
    /// </summary>
    [Route("/[controller]")]
    public class ModulationController : ControllerBase
    {
        private readonly IInstrument _instrument;

        public ModulationController(IInstrument instrument)
        {
            _instrument = instrument;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new ModulationDto
            {
                State = await new State.Get().ExecuteAsync(_instrument)
            };

            return Ok(result);
        }

        [HttpPatch]
        public async Task Set([FromBody] ModulationDto fragment)
        {
            if (fragment.State != null) await new State.Set(fragment.State.Value).ExecuteAsync(_instrument);
        }
    }
}
