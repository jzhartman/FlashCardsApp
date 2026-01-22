using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public class EditCardFrontTextHandler
{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;

    public EditCardFrontTextHandler(ICardRepository cardRepo, IStackRepository stackRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
    }

    public Result<string> Handle(CardResponse card, string editedText, string stackName, string cardSide)
    {
        var stackId = _stackRepo.GetIdByName(stackName);
        var cardId = _cardRepo.GetIdByTextAndStackId(stackId, card.FrontText, card.BackText);

        if (cardSide.ToUpper() == "FRONT" && string.IsNullOrWhiteSpace(editedText))
            return Result<string>.Success(card.FrontText);

        if (cardSide.ToUpper() == "BACK" && string.IsNullOrWhiteSpace(editedText))
            return Result<string>.Success(card.BackText);

        if (cardSide.ToUpper() == "FRONT" && _cardRepo.ExistsByFrontText(editedText, stackId))
            return Result<string>.Failure(Errors.CardFrontTextExists);

        if (cardSide.ToUpper() == "BACK" && _cardRepo.ExistsByBackText(editedText, stackId))
            return Result<string>.Failure(Errors.CardBackTextExists);

        return Result<string>.Success(editedText);
    }
}
