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

    public bool ExistsByName(string name)
    {
        var sql = @"select 1 from stack where UPPER(Name) = UPPER(@Name)";

        int exists = _dapper.Query<int>(_connection, sql, new { Name = name }).FirstOrDefault();

        return exists == 1 ? true : false;
    }

    public List<CardStack> GetAllStacks()
    {
        var sql = @"select s.Id, s.Name, c.Id as StackId, c.FrontText, c.BackText
                    from dbo.Stack s
                    left join dbo.Card c on s.Id = c.StackId";

        var lookup = new Dictionary<int, CardStack>();

        _dapper.Query<CardStack, Card, CardStack>(
            _connection,
            sql,
            (stack, card) =>
            {
                if (!lookup.TryGetValue(stack.Id, out var s))
                {
                    s = new CardStack(stack.Id, stack.Name, new List<Card>());
                    lookup.Add(s.Id, s);
                }

                if (card != null)
                    s.Cards.Add(card);
                return s;
            },
            splitOn: "StackId"
            );

        return lookup.Values.ToList();
    }

    public CardStack GetById(int id)
    {
        var sql = @"select name from Stack where Id = @Id";

        return _dapper.QuerySingle<CardStack>(_connection, sql);
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}
