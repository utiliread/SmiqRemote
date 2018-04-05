using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SmiqServer.Features.Frequency
{
    /// <summary>
    /// §2.4 RF Frequency
    /// </summary>
    [Route("/[controller]")]
    public class FrequencyController : ControllerBase
    {
        private readonly IInstrument _instrument;

        public FrequencyController(IInstrument instrument)
        {
            _instrument = instrument;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new FrequencyDto
            {
                Frequency = await new Builders.Frequency.Get().ExecuteAsync(_instrument),
            };

            return Ok(result);
        }

        [HttpPatch]
        public async Task Set([FromBody] FrequencyDto fragment)
        {
            if (fragment.Frequency != null) await new Builders.Frequency.Set(fragment.Frequency.Value).ExecuteAsync(_instrument);
        }
    }
}
