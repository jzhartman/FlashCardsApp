using FlashCards.Application.DTOs;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public interface IAddCardHandler
{
    Result<CardResponse> Handle(string stackName, string frontText, string backText);
}