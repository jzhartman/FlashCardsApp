using FlashCards.Core.Validation;

namespace FlashCards.Application.Interfaces;

public interface IAddStackHandler
{
    Result<string> Handle(string name);
}