using FlashCards.Core.Entities;

namespace FlashCards.Application.UseCases.Stacks;

public interface IGetStackById
{
    Stack Handle(int id);
}