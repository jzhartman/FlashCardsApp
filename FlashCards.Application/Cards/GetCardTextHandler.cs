using FlashCards.Application.Cards.EditTextBySide;
using FlashCards.Application.Enums;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Validation;

namespace FlashCards.Application.Cards;

public class GetCardTextHandler
{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;

    public GetCardTextHandler(ICardRepository cardRepo, IStackRepository stackRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
    }

    public Result<string> Handle(CardTextBySideCommand card)
    {
        var stackId = _stackRepo.GetIdByName(card.StackName);

        if (card.Side == CardSide.Front)
        {
            if (_cardRepo.ExistsByFrontText(card.Text, stackId))
                return Result<string>.Failure(Errors.CardFrontTextExists);

            if (string.IsNullOrWhiteSpace(card.Text))
                return Result<string>.Failure(Errors.CardFrontTextRequired);
        }
        if (card.Side == CardSide.Back)
        {
            if (_cardRepo.ExistsByBackText(card.Text, stackId))
                return Result<string>.Failure(Errors.CardBackTextExists);

            if (string.IsNullOrWhiteSpace(card.Text))
                return Result<string>.Failure(Errors.CardBackTextRequired);
        }

        return Result<string>.Success(card.Text);
    }
}
