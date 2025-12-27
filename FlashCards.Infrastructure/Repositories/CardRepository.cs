using Dapper;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FlashCards.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{

    private readonly IDbConnection _connection;

    public CardRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public int Add(Card card )
    {
        var sql = @"Insert into Card (StackId, FrontText, BackText)
                    Values (@StackId, @FrontText, @BackText)";

        return _connection.QuerySingle<int>(sql, card);
    }

    public void Delete(int id)
    {
        var sql = @"delete from Card
                    where Id = @id";

        _connection.Execute(sql, id);
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public Card GetById(int id)
    {
        var sql = @"select * from Card
                    where Id = @id";

        return _connection.Query<Card>(sql, id).FirstOrDefault();
    }
    public List<Card> GetAllByStackId(int id)
    {
        var sql = @"select * from Card
                    where StackId = @id";

        return _connection.Query<Card>(sql, id).ToList();
    }
}
