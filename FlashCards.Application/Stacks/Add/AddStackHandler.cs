using FlashCards.Application.Interfaces;
using FlashCards.Core.Validation;

namespace FlashCards.Application.Stacks.Add;

public class AddStackHandler
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

        if (name.Length > 32)
            return Result<string>.Failure(Errors.StackNameTooLong);

        _repo.Add(name);

        return Result<string>.Success(name);
    }

}