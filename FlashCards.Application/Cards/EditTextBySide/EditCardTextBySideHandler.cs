using FlashCards.Application.Enums;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Validation;

namespace FlashCards.Application.Cards.EditTextBySide;

public class EditCardTextBySideHandler
{
    private readonly ICardRepository _cardRepo;

    public EditCardTextBySideHandler(ICardRepository cardRepo)
    {
        _cardRepo = cardRepo;
    }

    public Result<string> Handle(CardResponse card, EditCardTextBySideCommand editedCard)
    {
        var cardId = _cardRepo.GetIdByTextAndStackId(card.StackId, card.FrontText, card.BackText);

        if (editedCard.Side == CardSide.Front)
        {
            if (string.IsNullOrWhiteSpace(editedCard.Text))
                return Result<string>.Success(card.FrontText);

            if (_cardRepo.ExistsByFrontTextExcludingId(editedCard.Text, card.StackId, cardId))
                return Result<string>.Failure(Errors.CardFrontTextExists);

            if (editedCard.Text.Length > 250)
                return Result<string>.Failure(Errors.CardTextLengthTooLong);
        }
        if (editedCard.Side == CardSide.Back)
        {
            if (string.IsNullOrWhiteSpace(editedCard.Text))
                return Result<string>.Success(card.BackText);

            if (_cardRepo.ExistsByBackTextExcludingId(editedCard.Text, card.StackId, cardId))
                return Result<string>.Failure(Errors.CardBackTextExists);

            if (editedCard.Text.Length > 250)
                return Result<string>.Failure(Errors.CardTextLengthTooLong);
        }

        return Result<string>.Success(editedCard.Text);
    }
}
