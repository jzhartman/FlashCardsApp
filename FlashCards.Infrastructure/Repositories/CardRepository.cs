using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using FlashCards.Infrastructure.Dapper;
using System.Data;

namespace FlashCards.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{

    private readonly IDbConnection _connection;
    private readonly IDapperWrapper _dapper;

    public CardRepository(IDbConnection connection, IDapperWrapper dapper)
    {
        _connection = connection;
        _dapper = dapper;
    }

    public int Add(Card card)
    {
        var sql = @"Insert into Card (StackId, FrontText, BackText)
                    Values (@StackId, @FrontText, @BackText);
                    Select cast(scope_identity() as int)";

        return _dapper.QuerySingle<int>(_connection, sql, card);
    }

    public void Delete(int id)
    {
        var sql = @"delete from Card
                    where Id = @id";

        _dapper.Execute(_connection, sql, id);
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public Card GetById(int id)
    {
        var sql = @"select * from Card
                    where Id = @Id";

        return _dapper.Query<Card>(_connection, sql, new { Id = id }).FirstOrDefault();
    }
    public List<Card> GetAllByStackId(int id)
    {
        var sql = @"select * from Card
                    where StackId = @id";

        return _dapper.Query<Card>(_connection, sql, id).ToList();
    }

    public bool ExistsByFrontText(string text, int stackId)
    {
        var sql = @"select 1 from card where UPPER(FrontText) = UPPER(@FrontText) AND StackId = @StackId";

        int exists = _dapper.Query<int>(_connection, sql, new { FrontText = text, StackId = stackId }).FirstOrDefault();

        return exists == 1 ? true : false;
    }

    public bool ExistsByBackText(string text, int stackId)
    {
        var sql = @"select 1 from card where UPPER(BackText) = UPPER(@BackText) AND StackId = @StackId";

        int exists = _dapper.Query<int>(_connection, sql, new { BackText = text, StackId = stackId }).FirstOrDefault();

        return exists == 1 ? true : false;
    }
}
