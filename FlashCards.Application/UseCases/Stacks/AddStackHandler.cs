using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Stacks;

public class AddStackHandler
{
    private readonly IStackRepository _repo;

    public AddStackHandler(IStackRepository repo)
    {
        _repo = repo;
    }

    public ValidationResult<CardStack> Handle(string name)
    {
        if (_repo.ExistsByName(name))
            return ValidationResult<CardStack>.Failure("Stack name must be unique!");

        if (String.IsNullOrWhiteSpace(name))
            return ValidationResult<CardStack>.Failure("Stack name cannot be empty!");

        var id = _repo.Add(name);
        var stack = new CardStack(id, name, new List<Card>());

        return ValidationResult<CardStack>.Success(stack);
    }

}