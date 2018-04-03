namespace SmiqServer
{
    public interface IQueryBuilder<TResult>
    {
        byte[] BuildQuery();

        TResult ParseResponse(byte[] response);
    }
}
