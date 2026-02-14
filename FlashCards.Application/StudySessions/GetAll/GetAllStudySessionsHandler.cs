using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using FlashCards.Core.Validation;

namespace FlashCards.Application.StudySessions.GetAll;

public class GetAllStudySessionsHandler
{
    private readonly IStackRepository _stackRepo;
    private readonly IStudySessionRepository _studyRepo;

    public GetAllStudySessionsHandler(IStackRepository stackRepo, IStudySessionRepository studyRepo)
    {
        _stackRepo = stackRepo;
        _studyRepo = studyRepo;
    }

    public Result<List<StudySessionResponse>> Handle()
    {
        var sessions = StudySessionMapper(_studyRepo.GetAll());

        if (sessions == null || sessions.Count == 0)
            return Result<List<StudySessionResponse>>.Failure(Errors.NoStudySessions);
        else
            return Result<List<StudySessionResponse>>.Success(sessions);
    }

    private List<StudySessionResponse> StudySessionMapper(List<StudySession> sessions)
    {
        var output = new List<StudySessionResponse>();

        foreach (var session in sessions)
        {
            var stack = _stackRepo.GetById(session.StackId);
            var sessionResponse = new StudySessionResponse(session.SessionDate, stack.Name, session.Score, session.CountStudied, session.CountCorrect, session.CountIncorrect);
            output.Add(sessionResponse);
        }
        return output;
    }
}
