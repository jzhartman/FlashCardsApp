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

    public Result<Stack> Handle(string name)
    {
        if (_repo.ExistsByName(name))
            return Result<Stack>.Failure("Stack name must be unique!");

        if (String.IsNullOrWhiteSpace(name))
            return Result<Stack>.Failure("Stack name cannot be empty!");

        var id = _repo.Add(name);
        var stack = new Stack(id, name, new List<Card>());

        return Result<Stack>.Success(stack);
    }

}