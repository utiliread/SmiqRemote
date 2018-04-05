using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SmiqServer.Features.Level.Builders;

namespace SmiqServer.Features.Level
{
    /// <summary>
    /// §2.5 RF Level
    /// </summary>
    [Route("/[controller]")]
    public class LevelController : ControllerBase
    {
        private readonly IInstrument _instrument;

        public LevelController(IInstrument instrument)
        {
            _instrument = instrument;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new LevelDto
            {
                Amplitude = await new Amplitude.Get().ExecuteAsync(_instrument),
                Offset = await new Offset.Get().ExecuteAsync(_instrument)
            };

            return Ok(result);
        }

        [HttpPatch]
        public async Task Set([FromBody] LevelDto fragment)
        {
            if (fragment.Amplitude != null) await new Amplitude.Set(fragment.Amplitude.Value).ExecuteAsync(_instrument);
            if (fragment.Offset != null) await new Offset.Set(fragment.Offset.Value).ExecuteAsync(_instrument);
        }
    }
}
