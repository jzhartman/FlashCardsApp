using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Stacks;

public interface IAddStackHandler
{
    Result<string> Handle(string name);
}