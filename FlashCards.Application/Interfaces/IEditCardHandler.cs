using FlashCards.Application.DTOs;

namespace FlashCards.Application.Interfaces;

public interface IEditCardHandler
{
    void Handle(CardResponse card, EditCardCommand editedCard);
}