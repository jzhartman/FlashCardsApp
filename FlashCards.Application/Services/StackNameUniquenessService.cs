using FlashCards.Application.Interfaces;

namespace FlashCards.Application.Services;

public class StackNameUniquenessService
{
    private readonly IStackRepository _repo;

    public StackNameUniquenessService(IStackRepository repo)
    {
        _repo = repo;
    }

    public bool IsStackNameUnique(string stackName)
    {
        return !_repo.ExistsByName(stackName);
    }
}
