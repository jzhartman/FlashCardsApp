using System.Data;

namespace FlashCards.Infrastructure.Dapper;

public interface IDapperWrapper
{
    T QuerySingle<T>(IDbConnection connection, string sql, object parameters = null);

    IEnumerable<T> Query<T>(IDbConnection connection, string sql, object parameters = null);

    void Execute(IDbConnection connection, string sql, object parameters = null);
}
