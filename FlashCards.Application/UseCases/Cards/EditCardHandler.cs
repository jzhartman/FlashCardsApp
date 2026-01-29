using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;

namespace FlashCards.Application.UseCases.Cards;

public class EditCardHandler : IEditCardHandler
{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;

    public EditCardHandler(ICardRepository cardRepo, IStackRepository stackRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
    }
    public void Handle(CardResponse card, EditCardCommand editedCard)
    {
        var stackId = _stackRepo.GetIdByName(editedCard.StackName);
        int id = _cardRepo.GetIdByTextAndStackId(stackId, card.FrontText, card.BackText);

        _cardRepo.UpdateCardText(id, editedCard.FrontText, editedCard.BackText);
    }
}
