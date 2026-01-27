using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.UseCases.StudySessions;

public class GetStudySessionByIdHandler
{
    private readonly IStackRepository _stackRepo;
    private readonly IStudySessionRepository _studyRepo;

    public GetStudySessionByIdHandler(IStackRepository stackRepo, IStudySessionRepository studyRepo)
    {
        _stackRepo = stackRepo;
        _studyRepo = studyRepo;
    }
    public StudySessionResponse Handle(StackNameAndCardCountResponse stack)
    {
        var stackId = _stackRepo.GetIdByName(stack.Name);
        var session = _studyRepo.GetByStackId(stackId);


        return StudySessionMapper(session, stack.Name);
    }

    private StudySessionResponse StudySessionMapper(StudySession session, string stackName)
    {
        return new StudySessionResponse(session.Time, stackName, session.Score, session.CountStudied, session.CountCorrect, session.CountIncorrect);
    }
}
