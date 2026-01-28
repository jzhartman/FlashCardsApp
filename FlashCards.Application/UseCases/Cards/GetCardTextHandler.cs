using FlashCards.Application.Enums;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public class GetCardTextHandler : IGetCardTextHandler
{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;

    public GetCardTextHandler(ICardRepository cardRepo, IStackRepository stackRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
    }

    public Result<string> Handle(string stackName, string text, CardSide cardSide)
    {
        var stackId = _stackRepo.GetIdByName(stackName);

        if (cardSide == CardSide.Front)
        {
            if (_cardRepo.ExistsByFrontText(text, stackId))
                return Result<string>.Failure(Errors.CardFrontTextExists);

            if (string.IsNullOrWhiteSpace(text))
                return Result<string>.Failure(Errors.CardFrontTextRequired);
        }
        if (cardSide == CardSide.Back)
        {
            if (_cardRepo.ExistsByBackText(text, stackId))
                return Result<string>.Failure(Errors.CardBackTextExists);

            if (string.IsNullOrWhiteSpace(text))
                return Result<string>.Failure(Errors.CardBackTextRequired);
        }

        return Result<string>.Success(text);
    }
}
