using FlashCards.Application.DTOs;

namespace FlashCards.Application.Interfaces;

public interface IGetAllCardsByStackName
{
    List<CardResponse> Handle(string name);
}