using FlashCards.Application.Interfaces;
using FlashCards.Application.Stacks.GetAll;

namespace FlashCards.Application.Stacks.Delete;

public class DeleteStackByNameHandler
{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;
    private readonly IStudySessionRepository _sessionRepo;

    public DeleteStackByNameHandler(ICardRepository cardRepo, IStackRepository stackRepo, IStudySessionRepository sessionRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
        _sessionRepo = sessionRepo;
    }

    public void Handle(StackNameAndCardCountResponse stack)
    {
        _cardRepo.DeleteAllByStackName(stack.Name);
        _sessionRepo.DeleteAllByStackName(stack.Name);
        _stackRepo.DeleteByName(stack.Name);
    }
}
