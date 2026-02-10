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

    public void DeleteById(int id)
    {
        var sql = @"delete from Card
                    where Id = @id";

        _dapper.Execute(_connection, sql, new { Id = id });
    }

    public void DeleteAllByStackName(string name)
    {
        var sql = @"delete c from Card c
                    inner join stack s on s.id = c.StackId
                    where s.Name = @name";

        _dapper.Execute(_connection, sql, new { Name = name });
    }
    public void DeleteAllByStackId(int id)
    {
        var sql = @"delete c from Card c
                    inner join stack s on s.id = c.StackId
                    where s.Id = @Id";

        _dapper.Execute(_connection, sql, new { Id = id });
    }

    public void UpdateCardText(int id, string frontText, string backText)
    {
        var sql = @"UPDATE card
                    SET FrontText = @frontText, BackText = @backText
                    WHERE Id = @id;";

        _dapper.Execute(_connection, sql, new { Id = id, FrontText = frontText, BackText = backText });
    }
    public void UpdateCardCounters(int id, int studied, int correct, int incorrect)
    {
        var sql = @"update card
                    set TimesStudied = TimesStudied +  @studied, TimesCorrect = TimesCorrect + @correct, TimesIncorrect = TimesIncorrect + @incorrect
                    where Id = @Id";

        _dapper.Execute(_connection, sql, new { Id = id, studied = studied, correct = correct, incorrect = incorrect });
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
    public List<Card> GetAllByStackName(string name)
    {
        var sql = @"select c.Id, c.FrontText, c.BackText, c.TimesStudied, c.TimesCorrect, c.TimesIncorrect
                    from Card c
                    inner join stack s on s.id = c.StackId
                    where s.Name = @name";

        return _dapper.Query<Card>(_connection, sql, new { Name = name }).ToList();
    }

    public int GetCardCountByStackName(string stackName)
    {
        var sql = @"select count(*)
                    from Card c
                    inner join stack s on s.id = c.stackid
                    where s.name = @StackName";

        return _dapper.Query<int>(_connection, sql, new { StackName = stackName }).First();
    }

    public int GetCardCountByStackId(int id)
    {
        var sql = @"select count(*)
                    from Card c
                    inner join stack s on s.id = c.stackid
                    where s.Id = @Id";

        return _dapper.Query<int>(_connection, sql, new { Id = id }).First();
    }

    public bool ExistsByFrontText(string text, int stackId)
    {
        var sql = @"select 1 from card where UPPER(FrontText) = UPPER(@FrontText) AND StackId = @StackId";

        int exists = _dapper.Query<int>(_connection, sql, new { FrontText = text, StackId = stackId }).FirstOrDefault();

        return exists == 1 ? true : false;
    }

    public bool ExistsByFrontTextExcludingId(string text, int stackId, int cardId)
    {
        var sql = @"select 1 from card where UPPER(FrontText) = UPPER(@FrontText) AND StackId = @StackId AND Id != @CardId";

        int exists = _dapper.Query<int>(_connection, sql, new { FrontText = text, StackId = stackId, CardId = cardId }).FirstOrDefault();

        return exists == 1 ? true : false;
    }

    public bool ExistsByBackText(string text, int stackId)
    {
        var sql = @"select 1 from card where UPPER(BackText) = UPPER(@BackText) AND StackId = @StackId";

        int exists = _dapper.Query<int>(_connection, sql, new { BackText = text, StackId = stackId }).FirstOrDefault();

        return exists == 1 ? true : false;
    }
    public bool ExistsByBackTextExcludingId(string text, int stackId, int cardId)
    {
        var sql = @"select 1 from card where UPPER(BackText) = UPPER(@BackText) AND StackId = @StackId AND Id != @CardId";

        int exists = _dapper.Query<int>(_connection, sql, new { BackText = text, StackId = stackId, CardId = cardId }).FirstOrDefault();

        return exists == 1 ? true : false;
    }
    public int GetIdByTextAndStackId(int stackId, string frontText, string backText)
    {
        var sql = @"select Id from card where UPPER(FrontText) = UPPER(@FrontText) AND UPPER(BackText) = UPPER(@BackText) AND StackId = @StackId";

        return _dapper.QuerySingle<int>(_connection, sql, new { StackId = stackId, FrontText = frontText, BackText = backText });
    }
}
