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
                    values (@Time, @StackId, @Score, @CountStudied, @CountCorrect, @CountIncorrect)";

        _dapper.Execute(_connection, sql, session);
    }
    public StudySession GetById(int id)
    {
        var sql = @"select * from StudySession
                    where Id = @Id";

        return _dapper.Query<StudySession>(_connection, sql, new { Id = id }).FirstOrDefault();
    }
    public List<StudySession> GetAll()
    {
        var sql = @"select * from Card";

        return _dapper.Query<StudySession>(_connection, sql).ToList();
    }
}
