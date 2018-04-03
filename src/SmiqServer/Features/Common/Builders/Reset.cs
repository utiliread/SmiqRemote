namespace SmiqServer.Features.Common.Builders
{
    public class Reset : CommandBuilder
    {
        public override string BuildPayload() => "*RST";
    }
}
