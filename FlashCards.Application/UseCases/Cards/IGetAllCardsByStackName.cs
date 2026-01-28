using FlashCards.Application.DTOs;

namespace FlashCards.Application.UseCases.Cards;

public interface IGetAllCardsByStackName
{
    List<CardResponse> Handle(string name);
}