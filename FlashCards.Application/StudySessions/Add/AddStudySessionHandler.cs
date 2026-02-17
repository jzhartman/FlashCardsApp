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

    public void Handle(AddStudySessionCommand session)
    {
        //var stackId = _stackRepo.GetIdByName(session.StackName);
        _studyRepo.Add(new StudySession
        {
            SessionDate = session.SessionDate,
            StackId = session.StackId,
            Score = session.Score,
            CountStudied = session.CardsStudied,
            CountCorrect = session.CardsCorrect,
            CountIncorrect = session.CardsIncorrect
        });

    }
}
