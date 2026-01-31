using FlashCards.Application.DTOs;
using FlashCards.Core.Validation;

namespace FlashCards.Application.Interfaces;

public interface IAddCardHandler
{
    Result<CardResponse> Handle(AddCardCommand cardCommand);
}