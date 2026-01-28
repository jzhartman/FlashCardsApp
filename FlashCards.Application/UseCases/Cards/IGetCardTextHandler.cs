using FlashCards.Application.Enums;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public interface IGetCardTextHandler
{
    Result<string> Handle(string stackName, string text, CardSide cardSide);
}