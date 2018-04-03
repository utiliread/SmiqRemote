namespace SmiqServer.Features.Common.Builders
{
    public class Trigger : CommandBuilder
    {
        public override string BuildPayload() => "*TRG";
    }
}
