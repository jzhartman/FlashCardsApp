using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FlashCards.Infrastructure.Dapper;

public interface IDapperWrapper
{
    T QuerySingle<T>(IDbConnection connection, string sql, object parameters = null);

    IEnumerable<T> Query<T>(IDbConnection connection, string sql, object parameters = null);

    void Execute(IDbConnection connection, string sql, object parameters = null);
}
