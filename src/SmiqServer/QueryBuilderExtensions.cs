using System.Threading.Tasks;

namespace SmiqServer
{
    public static class QueryBuilderExtensions
    {
        public static async Task<TResult> ExecuteAsync<TResult>(this IQueryBuilder<TResult> builder, IInstrument instrument)
        {
            var query = builder.BuildQuery();

            var response = await instrument.QueryAsync(query);

            return builder.ParseResponse(response);
        }
    }
}
