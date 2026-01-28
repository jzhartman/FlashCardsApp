using FlashCards.Application.DTOs;

namespace FlashCards.Application.UseCases.Stacks;

public interface IDeleteStackByNameHandler
{
    void Handle(StackNameAndCardCountResponse stack);
}