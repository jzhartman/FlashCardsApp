using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using FlashCards.Infrastructure.Dapper;
using System.Data;

namespace FlashCards.Infrastructure.Repositories;

public class StackRepository : IStackRepository
{
    private readonly IDbConnection _connection;
    private readonly IDapperWrapper _dapper;

    public StackRepository(IDbConnection connection, IDapperWrapper dapper)
    {
        _connection = connection;
        _dapper = dapper;
    }

    public int Add(string name)
    {
        var sql = @"insert into Stack (Name)
                    values (@Name);
                    select cast(scope_identity() as int)";

        return _dapper.QuerySingle<int>(_connection, sql, new { Name = name });
    }


    public void DeleteById(int id)
    {
        var sql = @"delete from Stack
                    where Id = @id";

        _dapper.Execute(_connection, sql, new { Id = id });
    }


    public List<Stack> GetAll()
    {
        var sql = @"select Id, Name
                    from Stack";

        return _dapper.Query<Stack>(_connection, sql).ToList();
    }
    public Stack GetById(int id)
    {
        var sql = @"select name from Stack where Id = @Id";

        return _dapper.QuerySingle<Stack>(_connection, sql, new { Id = id });
    }
    public int GetIdByName(string name)
    {
        var sql = @"select Id from Stack where Name = @Name";

        return _dapper.QuerySingle<int>(_connection, sql, new { Name = name });
    }


    public bool ExistsByName(string name)
    {
        var sql = @"select 1 from stack where UPPER(Name) = UPPER(@Name)";

        int exists = _dapper.Query<int>(_connection, sql, new { Name = name }).FirstOrDefault();

        return exists == 1 ? true : false;
    }
    public bool ExistsById(int id)
    {
        var sql = @"select 1 from stack where Id = @Id";

        int exists = _dapper.Query<int>(_connection, sql, new { Id = id }).FirstOrDefault();

        return exists == 1 ? true : false;
    }
}
