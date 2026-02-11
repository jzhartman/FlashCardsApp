using FlashCards.Application.Cards.EditTextBySide;
using FlashCards.Application.Interfaces;

namespace FlashCards.Application.Cards.EditCard;

public class EditCardHandler
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
        int id = _cardRepo.GetIdByTextAndStackId(card.StackId, card.FrontText, card.BackText);

        _cardRepo.UpdateCardText(id, editedCard.FrontText, editedCard.BackText);
    }
}
