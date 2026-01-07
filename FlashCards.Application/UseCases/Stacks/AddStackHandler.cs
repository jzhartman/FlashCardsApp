using FlashCards.Application.Interfaces;
using FlashCards.Application.Services;

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

    public int HandleAdd(string name)
    {
        // Build in validation with a ValidationResult object

        if (_nameUniqueness.IsStackNameUnique(name) == true)
            return _repo.Add(name);
        else return -1;
    }

}