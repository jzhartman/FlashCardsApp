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
        var sql = @"insert into StudySession (SessionDate, StackId, Score, CountStudied, CountCorrect, CountIncorrect)
                    values (@SessionDate, @StackId, @Score, @CountStudied, @CountCorrect, @CountIncorrect)";

        _dapper.Execute(_connection, sql, session);
    }


    public void DeleteAllByStackId(int id)
    {
        var sql = @"delete u from StudySession u
                    inner join stack s on s.id = u.StackId
                    where s.Id = @Id";

        _dapper.Execute(_connection, sql, new { Id = id });
    }


    public List<StudySession> GetAll()
    {
        var sql = @"select * from StudySession";

        return _dapper.Query<StudySession>(_connection, sql).ToList();
    }
    public int GetSessionCountByStackId(int id)
    {
        var sql = @"select count(*)
                    from StudySession t
                    inner join stack s on s.id = t.StackId
                    where s.Id = @Id";

        return _dapper.Query<int>(_connection, sql, new { Id = id }).First();
    }

    public List<AverageScoreReport> GetAverageScoreByMonth()
    {
        var sql = @"WITH newTable AS(
	                    SELECT ss.StackId, s.Name as StackName, ss.Score, Year(ss.SessionDate) as SessionYear, Month(ss.SessionDate) as SessionMonth
	                    FROM StudySession ss
	                    inner join Stack s on s.Id = ss.StackId),
                    Pivoted AS(
	                    select * from newTable
	                    pivot(
		                    avg(score)
		                    for SessionMonth in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])
		                    ) as P)
                    select StackId, StackName, SessionYear, [1] as January,[2] as February,[3] as March,[4] as April,[5] as May,[6] as June,[7] as July,[8] as August,[9] as September,[10] as October,[11] as November,[12] as December
                    from Pivoted";

        return _dapper.Query<AverageScoreReport>(_connection, sql).ToList();
    }
}
