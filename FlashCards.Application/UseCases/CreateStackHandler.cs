using FlashCards.Application.Interfaces;

namespace FlashCards.Application.UseCases;

public class CreateStackHandler
{
    private readonly IStackRepository _repo;

    public CreateStackHandler(IStackRepository repo)
    {
        _repo = repo;
    }

    public int Handle(string name)
    {
        // Validate Name
        var checkName = StackNameIsUnique();

        if (checkName)
        {
            return _repo.Add(name);
        }
        else return -1;
    }

    private bool StackNameIsUnique()
    {
        // Check repo for name
        return true;
    }
}