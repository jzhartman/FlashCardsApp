using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;

namespace FlashCards.Application.UseCases.Stacks;

public class DeleteStackByNameHandler : IDeleteStackByNameHandler
{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;

    public DeleteStackByNameHandler(ICardRepository cardRepo, IStackRepository stackRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
    }

    public void Handle(StackNameAndCardCountResponse stack)
    {
        _cardRepo.DeleteAllByStackName(stack.Name);
        _stackRepo.DeleteByName(stack.Name);
    }
}
