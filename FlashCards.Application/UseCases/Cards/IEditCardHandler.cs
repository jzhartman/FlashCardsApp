using FlashCards.Application.DTOs;

namespace FlashCards.Application.UseCases.Cards;

public interface IEditCardHandler
{
    void Handle(CardResponse card, EditCardCommand editedCard);
}