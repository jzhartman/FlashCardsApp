using FlashCards.Application.Enums;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Validation;

namespace FlashCards.Application.Cards.ValidateCardTextBySide;

public class ValidateCardTextBySide
{
    private readonly ICardRepository _cardRepo;

    public ValidateCardTextBySide(ICardRepository cardRepo)
    {
        _cardRepo = cardRepo;
    }

    public Result<string> Handle(ValidateCardTextBySideCommand card)
    {
        if (card.Side == CardSide.Front)
        {
            if (_cardRepo.ExistsByFrontText(card.Text, card.StackId))
                return Result<string>.Failure(Errors.CardFrontTextExists);

            if (string.IsNullOrWhiteSpace(card.Text))
                return Result<string>.Failure(Errors.CardFrontTextRequired);

            if (card.Text.Length > 250)
                return Result<string>.Failure(Errors.CardTextLengthTooLong);
        }
        if (card.Side == CardSide.Back)
        {
            if (_cardRepo.ExistsByBackText(card.Text, card.StackId))
                return Result<string>.Failure(Errors.CardBackTextExists);

            if (string.IsNullOrWhiteSpace(card.Text))
                return Result<string>.Failure(Errors.CardBackTextRequired);

            if (card.Text.Length > 250)
                return Result<string>.Failure(Errors.CardTextLengthTooLong);
        }

        return Result<string>.Success(card.Text);
    }
}
