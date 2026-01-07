using FlashCards.Application.Interfaces;
using FlashCards.Application.Services;
using FlashCards.Core.Entities;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Stacks;

public class AddStackHandler
{
    private readonly IStackRepository _repo;
    private readonly StackNameUniquenessService _nameUniqueness;

    public AddStackHandler(IStackRepository repo, StackNameUniquenessService nameUniqueness)
    {
        _repo = repo;
        _nameUniqueness = nameUniqueness;
    }

    public ValidationResult<Stack> HandleAdd(string name)
    {
        // Build in validation with a ValidationResult object
        if (_nameUniqueness.IsStackNameUnique(name) == false)
            return ValidationResult<Stack>.Failure("Stack name must be unique!");

        if (String.IsNullOrWhiteSpace(name))
            return ValidationResult<Stack>.Failure("Stack name cannot be empty!");

        var id = _repo.Add(name);
        var stack = new Stack { Name = name, Id = id };

        return ValidationResult<Stack>.Success(stack);
    }

}