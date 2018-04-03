namespace SmiqServer.Features.DigitalModulation
{
    public class DigitalModulationDto
    {
        public string ControlList { get; set; }
        public string DataList { get; set; }
        public FilterType? Filter { get; set; }
        public FormatType? Format { get; set; }
        public decimal? FskDeviation { get; set; }
        public SequenceType? Sequence { get; set; }
        public SourceType? Source { get; set; }
        public bool? State { get; set; }
        public decimal? SymbolRate { get; set; }
    }
}
