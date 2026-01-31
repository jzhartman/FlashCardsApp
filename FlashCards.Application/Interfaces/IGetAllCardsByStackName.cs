using FlashCards.Application.DTOs;
using FlashCards.Core.Validation;

namespace FlashCards.Application.Interfaces;

public interface IGetAllCardsByStackName
{
    Result<List<CardResponse>> Handle(string name);
}