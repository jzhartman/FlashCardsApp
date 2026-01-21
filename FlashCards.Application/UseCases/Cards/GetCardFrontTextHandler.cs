using FlashCards.Application.Interfaces;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public class GetCardFrontTextHandler
{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;

    public GetCardFrontTextHandler(ICardRepository cardRepo, IStackRepository stackRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
    }

    public Result<string> Handle(string stackName, string text, string cardSide)
    {
        var stackId = _stackRepo.GetIdByName(stackName);

        if (cardSide.ToLower() == "front")
        {
            if (_cardRepo.ExistsByFrontText(text, stackId))
                return Result<string>.Failure(Errors.CardFrontTextExists);

            if (string.IsNullOrWhiteSpace(text))
                return Result<string>.Failure(Errors.CardFrontTextRequired);
        }
        if (cardSide.ToLower() == "back")
        {
            if (_cardRepo.ExistsByBackText(text, stackId))
                return Result<string>.Failure(Errors.CardBackTextExists);

            if (string.IsNullOrWhiteSpace(text))
                return Result<string>.Failure(Errors.CardBackTextRequired);
        }

        return Result<string>.Success(text);
    }
}
