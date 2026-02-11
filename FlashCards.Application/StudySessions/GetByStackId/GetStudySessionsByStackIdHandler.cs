using FlashCards.Application.Interfaces;
using FlashCards.Application.Stacks.GetAll;
using FlashCards.Core.Entities;

namespace FlashCards.Application.StudySessions.GetByStackId;

public class GetStudySessionsByStackIdHandler
{
    private readonly IStudySessionRepository _studyRepo;

    public GetStudySessionsByStackIdHandler(IStudySessionRepository studyRepo)
    {
        _studyRepo = studyRepo;
    }
    public List<StudySessionResponse> Handle(StackNamesWithCountsResponse stack)
    {
        var sessions = _studyRepo.GetAllByStackId(stack.Id);

        return StudySessionMapper(sessions, stack.Name);
    }

    private List<StudySessionResponse> StudySessionMapper(List<StudySession> sessions, string stackName)
    {
        var output = new List<StudySessionResponse>();

        foreach (var session in sessions)
        {
            output.Add(new StudySessionResponse(session.Time, stackName, session.Score, session.CountStudied, session.CountCorrect, session.CountIncorrect));
        }

        return output;
    }
}
