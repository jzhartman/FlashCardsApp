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
        throw new NotImplementedException();
    }

    public bool ExistsByName()
    {
        var sql = @"";

        return true;
    }

    public List<Stack> GetAllStacks()
    {
        var sql = @"select Name from Stacks";

        return _dapper.Query<Stack>(_connection, sql).ToList();
    }

    public Stack GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}
