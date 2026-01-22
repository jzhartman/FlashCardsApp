using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;

namespace FlashCards.Application.UseCases.Cards;

public class EditCardHandler
{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;

    public EditCardHandler(ICardRepository cardRepo, IStackRepository stackRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
    }
    public void Handle(string stackName, CardResponse card, string frontText, string backText)
    {
        var stackId = _stackRepo.GetIdByName(stackName);
        int id = _cardRepo.GetIdByTextAndStackId(stackId, card.FrontText, card.BackText);

        _cardRepo.UpdateCardText(id, frontText, backText);
    }
}
