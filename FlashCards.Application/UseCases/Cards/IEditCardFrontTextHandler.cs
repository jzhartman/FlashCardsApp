using FlashCards.Application.DTOs;
using FlashCards.Application.Enums;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public interface IEditCardFrontTextHandler
{
    Result<string> Handle(CardResponse card, string editedText, string stackName, CardSide cardSide);
}