namespace SmiqServer.Features.DigitalModulation
{
    public class DigitalModulationDto
    {
        public bool? State { get; set; }
        public SourceDto Source { get; set; }
        public ModulationDto Modulation { get; set; }
        public decimal? SymbolRate { get; set; }
        public FilterDto Filter { get; set; }
        public TriggerModeType? TriggerMode { get; set; }
    }
}
