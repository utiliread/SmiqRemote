namespace SmiqServer.Features.Common.Builders
{
    public class Identification : QueryBuilder<string>
    {
        public override string BuildPayload() => "*IDN?";

        public override string ParsePayload(string response) => response;
    }
}
