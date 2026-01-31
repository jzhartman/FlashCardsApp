using FlashCards.Core.Entities;

namespace FlashCards.Application.Interfaces;

public interface IGetStackById
{
    Stack Handle(int id);
}