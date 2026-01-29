using FlashCards.Application.DTOs;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public interface IGetCardTextHandler
{
    Result<string> Handle(CardTextBySideCommand card);
}