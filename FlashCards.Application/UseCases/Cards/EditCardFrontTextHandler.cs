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

    public Result<string> Handle(CardResponse card, string editedFrontText, string stackName)
    {
        var stackId = _stackRepo.GetIdByName(stackName);

        if (string.IsNullOrWhiteSpace(editedFrontText))
            return Result<string>.Success(card.FrontText);

        if (_cardRepo.ExistsByFrontText(editedFrontText, stackId))
            return Result<string>.Failure(Errors.CardFrontTextExists);


        return Result<string>.Success(editedFrontText);
    }
}
