using System.Data;

namespace FlashCards.Infrastructure.Dapper;

public interface IDapperWrapper
{
    T QuerySingle<T>(IDbConnection connection, string sql, object parameters = null);

    IEnumerable<T> Query<T>(IDbConnection connection, string sql, object parameters = null);

    IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(
        IDbConnection connection,
        string sql,
        Func<TFirst, TSecond, TReturn> map,
        object parameters = null,
        string splitOn = "Id");

    void Execute(IDbConnection connection, string sql, object parameters = null);
}
