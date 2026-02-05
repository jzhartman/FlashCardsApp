using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

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
    public List<StudySessionResponse> Handle()
    {
        return StudySessionMapper(_studyRepo.GetAll());
    }

    private List<StudySessionResponse> StudySessionMapper(List<StudySession> sessions)
    {
        var output = new List<StudySessionResponse>();

        foreach (var session in sessions)
        {
            var stack = _stackRepo.GetById(session.StackId);
            var sessionResponse = new StudySessionResponse(session.Time, stack.Name, session.Score, session.CountStudied, session.CountCorrect, session.CountIncorrect);
            output.Add(sessionResponse);
        }
        return output;
    }
}
