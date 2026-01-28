using FlashCards.Application.DTOs;

namespace FlashCards.Application.UseCases.Cards;

public interface IEditCardHandler
{
    void Handle(string stackName, CardResponse card, string frontText, string backText);
}