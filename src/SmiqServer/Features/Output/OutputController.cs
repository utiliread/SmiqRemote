using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SmiqServer.Features.Output.Builders;

namespace SmiqServer.Features.Output
{
    /// <summary>
    /// §3.5.12 Output System
    /// </summary>
    [Route("/[controller]")]
    public class OutputController : ControllerBase
    {
        private readonly IInstrument _instrument;

        public OutputController(IInstrument instrument)
        {
            _instrument = instrument;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new OutputDto()
            {
                State = await new State.Get().ExecuteAsync(_instrument)
            };

            return Ok(result);
        }

        [HttpPatch]
        public async Task Set([FromBody] OutputDto fragment)
        {
            if (fragment.State != null) await new State.Set(fragment.State.Value).ExecuteAsync(_instrument);
        }
    }
}
