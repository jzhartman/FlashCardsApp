using FlashCards.Application.Interfaces;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Stacks;

public class AddStackHandler : IAddStackHandler
{
    private readonly IStackRepository _repo;

    public AddStackHandler(IStackRepository repo)
    {
        _repo = repo;
    }

    public Result<string> Handle(string name)
    {
        if (_repo.ExistsByName(name))
            return Result<string>.Failure(Errors.StackNameExists);

        if (String.IsNullOrWhiteSpace(name))
            return Result<string>.Failure(Errors.StackNameRequired);

        _repo.Add(name);

        return Result<string>.Success(name);
    }

}