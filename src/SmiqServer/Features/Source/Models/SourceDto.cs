namespace SmiqServer.Features.Source
{
    public class SourceDto
    {
        public decimal? Frequency { get; set; }
        public bool? Modulation { get; set; }
        public decimal? Power { get; set; }
        public SequenceType? Sequence { get; set; }
        public SourceType? Source { get; set; }
    }
}
