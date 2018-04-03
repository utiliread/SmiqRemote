using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SmiqServer.Features.Source.Builders;

namespace SmiqServer.Features.Source
{
    /// <summary>
    /// §3.5.14 Source System
    /// </summary>
    [Route("/[controller]")]
    public partial class SourceController : ControllerBase
    {
        private readonly IInstrument _instrument;

        public SourceController(IInstrument instrument)
        {
            _instrument = instrument;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new SourceDto
            {
                Frequency = await new Frequency.Get().ExecuteAsync(_instrument),
                Modulation = await new Modulation.Get().ExecuteAsync(_instrument),
                Power = await new Power.Get().ExecuteAsync(_instrument),
                Sequence = await new Sequence.Get().ExecuteAsync(_instrument),
                Source = await new Builders.Source.Get().ExecuteAsync(_instrument)
            };

            return Ok(result);
        }

        [HttpPatch]
        public async Task Set([FromBody] SourceDto fragment)
        {
            if (fragment.Frequency != null) await new Frequency.Set(fragment.Frequency.Value).ExecuteAsync(_instrument);
            if (fragment.Modulation != null) await new Modulation.Set(fragment.Modulation.Value).ExecuteAsync(_instrument);
            if (fragment.Power != null) await new Power.Set(fragment.Power.Value).ExecuteAsync(_instrument);
            if (fragment.Sequence != null) await new Sequence.Set(fragment.Sequence.Value).ExecuteAsync(_instrument);
            if (fragment.Source != null) await new Builders.Source.Set(fragment.Source.Value).ExecuteAsync(_instrument);
        }
    }
}
