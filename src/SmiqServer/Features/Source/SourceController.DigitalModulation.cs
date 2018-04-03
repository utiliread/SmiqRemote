﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SmiqServer.Features.Source.Builders.DigitalModulation;

namespace SmiqServer.Features.Source
{
    public partial class SourceController
    {
        [HttpGet("DigitalModulation")]
        public async Task<IActionResult> DigitalModulation()
        {
            var result = new DigitalModulationDto()
            {
                ControlList = await new ControlList.Get().ExecuteAsync(_instrument),
                DataList = await new DataList.Get().ExecuteAsync(_instrument),
                Filter = await new Filter.Get().ExecuteAsync(_instrument),
                Format = await new Format.Get().ExecuteAsync(_instrument),
                FskDeviation = await new FskDeviation.Get().ExecuteAsync(_instrument),
                State = await new State.Get().ExecuteAsync(_instrument),
                SymbolRate = await new SymbolRate.Get().ExecuteAsync(_instrument)
            };

            return Ok(result);
        }

        [HttpPatch("DigitalModulation")]
        public async Task<IActionResult> Set([FromBody] DigitalModulationDto fragment)
        {
            if (fragment.ControlList != null) await new ControlList.Set(fragment.ControlList).ExecuteAsync(_instrument);
            if (fragment.DataList != null) await new DataList.Set(fragment.DataList).ExecuteAsync(_instrument);
            if (fragment.Filter != null) await new Filter.Set(fragment.Filter.Value).ExecuteAsync(_instrument);
            if (fragment.Format != null) await new Format.Set(fragment.Format.Value).ExecuteAsync(_instrument);
            if (fragment.FskDeviation != null) await new FskDeviation.Set(fragment.FskDeviation.Value).ExecuteAsync(_instrument);
            if (fragment.State != null) await new State.Set(fragment.State.Value).ExecuteAsync(_instrument);
            if (fragment.SymbolRate != null) await new SymbolRate.Set(fragment.SymbolRate.Value).ExecuteAsync(_instrument);

            return Ok();
        }
    }
}
