using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using FlashCards.Infrastructure.Dapper;
using System.Data;

namespace FlashCards.Infrastructure.Repositories;

public class StudySessionRepository : IStudySessionRepository
{
    private readonly IDbConnection _connection;
    private readonly IDapperWrapper _dapper;

    public StudySessionRepository(IDbConnection connection, IDapperWrapper dapper)
    {
        _connection = connection;
        _dapper = dapper;
    }

    public void Add(StudySession session)
    {
        var sql = @"insert into StudySession (Time, StackId, Score, CountStudied, CountCorrect, CountIncorrect)
                    values (@Time, @StackId, @Score, @CountStudied, @CountCorrect, @CountIncorrect);
                    select cast(scope_identity() as int)";

        _dapper.Execute(_connection, sql, new { session });
    }
}
