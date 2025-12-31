using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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

    public T QuerySingle<T>(IDbConnection connection, string sql, object parameters = null)
    {
        return connection.QuerySingle<T>(sql, parameters);
    }
}
