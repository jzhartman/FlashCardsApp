using FlashCards.Application.DTOs;

namespace FlashCards.Application.Interfaces;

public interface IDeleteStackByNameHandler
{
    void Handle(StackNameAndCardCountResponse stack);
}