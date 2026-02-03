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
    public List<StudySession> GetAllByStackId(int stackId)
    {
        var sql = @"select * from StudySession
                    where StackId = @StackId";

        return _dapper.Query<StudySession>(_connection, sql, new { StackId = stackId }).ToList();
    }
    public List<StudySession> GetAll()
    {
        var sql = @"select * from StudySession";

        return _dapper.Query<StudySession>(_connection, sql).ToList();
    }

    public void DeleteAllByStackName(string name)
    {
        var sql = @"delete u from StudySession u
                    inner join stack s on s.id = u.StackId
                    where s.Name = @name";

        _dapper.Execute(_connection, sql, new { Name = name });
    }
}
