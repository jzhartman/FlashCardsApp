using FlashCards.Application.DTOs;
using FlashCards.Application.Enums;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public class EditCardFrontTextHandler : IEditCardFrontTextHandler
{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;

    public EditCardFrontTextHandler(ICardRepository cardRepo, IStackRepository stackRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
    }

    public Result<string> Handle(CardResponse card, string editedText, string stackName, CardSide cardSide)
    {
        var stackId = _stackRepo.GetIdByName(stackName);
        var cardId = _cardRepo.GetIdByTextAndStackId(stackId, card.FrontText, card.BackText);

        if (cardSide == CardSide.Front)
        {
            if (string.IsNullOrWhiteSpace(editedText)) return Result<string>.Success(card.FrontText);
            if (_cardRepo.ExistsByFrontTextExcludingId(editedText, stackId, cardId)) return Result<string>.Failure(Errors.CardFrontTextExists);
        }
        if (cardSide == CardSide.Back)
        {
            if (string.IsNullOrWhiteSpace(editedText)) return Result<string>.Success(card.BackText);
            if (_cardRepo.ExistsByBackTextExcludingId(editedText, stackId, cardId)) return Result<string>.Failure(Errors.CardBackTextExists);
        }

        return Result<string>.Success(editedText);
    }
}
