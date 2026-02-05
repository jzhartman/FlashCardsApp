using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.StudySessions.Add;

public class AddStudySessionHandler
{
    private readonly IStudySessionRepository _studyRepo;
    private readonly IStackRepository _stackRepo;

    public AddStudySessionHandler(IStudySessionRepository repo, IStackRepository stackRepo)
    {
        _studyRepo = repo;
        _stackRepo = stackRepo;
    }

    public void Handle(StudySessionResponse session)
    {
        var stackId = _stackRepo.GetIdByName(session.StackName);
        _studyRepo.Add(new StudySession
        {
            Time = session.Time,
            StackId = stackId,
            Score = session.Score,
            CountStudied = session.CountStudied,
            CountCorrect = session.CountCorrect,
            CountIncorrect = session.CountIncorrect
        });

    }
}
