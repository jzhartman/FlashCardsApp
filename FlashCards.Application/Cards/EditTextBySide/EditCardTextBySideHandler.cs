using FlashCards.Application.Enums;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Validation;

namespace FlashCards.Application.Cards.EditTextBySide;

public class EditCardTextBySideHandler
{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;

    public EditCardTextBySideHandler(ICardRepository cardRepo, IStackRepository stackRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
    }

    public Result<string> Handle(CardResponse card, CardTextBySideCommand editedCard)
    {
        var stackId = _stackRepo.GetIdByName(editedCard.StackName);
        var cardId = _cardRepo.GetIdByTextAndStackId(stackId, card.FrontText, card.BackText);

        if (editedCard.Side == CardSide.Front)
        {
            if (string.IsNullOrWhiteSpace(editedCard.Text)) return Result<string>.Success(card.FrontText);
            if (_cardRepo.ExistsByFrontTextExcludingId(editedCard.Text, stackId, cardId)) return Result<string>.Failure(Errors.CardFrontTextExists);
        }
        if (editedCard.Side == CardSide.Back)
        {
            if (string.IsNullOrWhiteSpace(editedCard.Text)) return Result<string>.Success(card.BackText);
            if (_cardRepo.ExistsByBackTextExcludingId(editedCard.Text, stackId, cardId)) return Result<string>.Failure(Errors.CardBackTextExists);
        }

        return Result<string>.Success(editedCard.Text);
    }
}
