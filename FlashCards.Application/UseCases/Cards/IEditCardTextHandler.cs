using FlashCards.Application.DTOs;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public interface IEditCardTextHandler
{
    Result<string> Handle(CardResponse card, CardTextBySideCommand editedCard);
}