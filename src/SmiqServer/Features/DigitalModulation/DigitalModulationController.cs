using Microsoft.AspNetCore.Mvc;
using SmiqServer.Features.DigitalModulation.Builders;
using System.Threading.Tasks;

namespace SmiqServer.Features.DigitalModulation
{
    /// <summary>
    /// §2.10.9 Digital Modulation Menu
    /// </summary>
    [Route("/[controller]")]
    public partial class DigitalModulationController : ControllerBase
    {
        private readonly IInstrument _instrument;

        public DigitalModulationController(IInstrument instrument)
        {
            _instrument = instrument;
        }

        [HttpGet]
        public async Task<IActionResult> DigitalModulation()
        {
            var result = new DigitalModulationDto()
            {
                State = await new State.Get().ExecuteAsync(_instrument),
                Source = new SourceDto()
                {
                    Source = await new Builders.Source.Get().ExecuteAsync(_instrument),
                    DataList = await new DataList.Get().ExecuteAsync(_instrument),
                    ControlList = await new ControlList.Get().ExecuteAsync(_instrument),
                },
                Modulation = new ModulationDto()
                {
                    Type = await new Builders.Modulation.Get().ExecuteAsync(_instrument),
                    FskDeviation = await new FskDeviation.Get().ExecuteAsync(_instrument)
                },
                SymbolRate = await new SymbolRate.Get().ExecuteAsync(_instrument),
                Filter = new FilterDto()
                {
                    Type = await new Filter.Get().ExecuteAsync(_instrument),
                },
                TriggerMode = await new TriggerMode.Get().ExecuteAsync(_instrument),
            };

            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> Set([FromBody] DigitalModulationDto fragment)
        {
            if (fragment.State != null) await new State.Set(fragment.State.Value).ExecuteAsync(_instrument);
            if (fragment.Source?.Source != null) await new Builders.Source.Set(fragment.Source.Source.Value).ExecuteAsync(_instrument);
            if (fragment.Source?.DataList != null) await new DataList.Set(fragment.Source.DataList).ExecuteAsync(_instrument);
            if (fragment.Source?.ControlList != null) await new ControlList.Set(fragment.Source.ControlList).ExecuteAsync(_instrument);
            if (fragment.Modulation?.Type != null) await new Builders.Modulation.Set(fragment.Modulation.Type.Value).ExecuteAsync(_instrument);
            if (fragment.Modulation?.FskDeviation != null) await new FskDeviation.Set(fragment.Modulation.FskDeviation.Value).ExecuteAsync(_instrument);
            if (fragment.SymbolRate != null) await new SymbolRate.Set(fragment.SymbolRate.Value).ExecuteAsync(_instrument);
            if (fragment.Filter?.Type != null) await new Filter.Set(fragment.Filter.Type.Value).ExecuteAsync(_instrument);
            if (fragment.TriggerMode != null) await new TriggerMode.Set(fragment.TriggerMode.Value).ExecuteAsync(_instrument);

            return Ok();
        }
    }
}
