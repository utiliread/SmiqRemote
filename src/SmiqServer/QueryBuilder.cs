using System.Text;

namespace SmiqServer
{
    public abstract class QueryBuilder<TResult> : IQueryBuilder<TResult>
    {
        public byte[] BuildQuery()
        {
            var payload = BuildPayload();

            return Encoding.ASCII.GetBytes(payload + "\n");
        }

        public TResult ParseResponse(byte[] response)
        {
            return ParsePayload(Encoding.ASCII.GetString(response));
        }

        public abstract string BuildPayload();

        public abstract TResult ParsePayload(string response);
    }
}
