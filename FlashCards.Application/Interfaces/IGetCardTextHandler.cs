using FlashCards.Application.DTOs;
using FlashCards.Core.Validation;

namespace FlashCards.Application.Interfaces;

public interface IGetCardTextHandler
{
    Result<string> Handle(CardTextBySideCommand card);
}