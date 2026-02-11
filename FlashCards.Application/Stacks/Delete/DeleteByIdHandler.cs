using FlashCards.Application.Interfaces;
using FlashCards.Application.Stacks.GetAll;

namespace FlashCards.Application.Stacks.Delete;

public class DeleteByIdHandler
{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;
    private readonly IStudySessionRepository _sessionRepo;

    public DeleteByIdHandler(ICardRepository cardRepo, IStackRepository stackRepo, IStudySessionRepository sessionRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
        _sessionRepo = sessionRepo;
    }

    public void Handle(StackNamesWithCountsResponse stack)
    {
        _cardRepo.DeleteAllByStackId(stack.Id);
        _sessionRepo.DeleteAllByStackId(stack.Id);
        _stackRepo.DeleteById(stack.Id);
    }
}
