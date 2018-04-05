using System;
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
            if (response[response.Length - 1] != '\n')
            {
                throw new ArgumentException();
            }

            return ParsePayload(Encoding.ASCII.GetString(response, 0, response.Length - 1));
        }

        public abstract string BuildPayload();

        public abstract TResult ParsePayload(string response);
    }
}
