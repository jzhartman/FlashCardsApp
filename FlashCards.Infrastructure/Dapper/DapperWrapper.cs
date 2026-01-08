using Dapper;
using System.Data;

namespace FlashCards.Infrastructure.Dapper;

public class DapperWrapper : IDapperWrapper
{
    public void Execute(IDbConnection connection, string sql, object parameters = null)
    {
        connection.Execute(sql, parameters);
    }

    public IEnumerable<T> Query<T>(IDbConnection connection, string sql, object parameters = null)
    {
        return connection.Query<T>(sql, parameters);
    }

    public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(
        IDbConnection connection,
        string sql,
        Func<TFirst, TSecond, TReturn> map,
        object parameters = null,
        string splitOn = "Id")
    {
        return connection.Query<TFirst, TSecond, TReturn>(sql, map, parameters, splitOn: splitOn);
    }

    public T QuerySingle<T>(IDbConnection connection, string sql, object parameters = null)
    {
        return connection.QuerySingle<T>(sql, parameters);
    }
}
