using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.UseCases;

public class StackHandler
{
    private readonly IStackRepository _repo;

    public StackHandler(IStackRepository repo)
    {
        _repo = repo;
    }

    public int HandleAdd(string name)
    {
        // Validate Name
        var checkName = StackNameIsUnique();

        if (checkName)
        {
            return _repo.Add(name);
        }
        else return -1;
    }
    public List<Stack> HandleGetAll()
    {
        return _repo.GetAllStacks();
    }

    private bool StackNameIsUnique()
    {
        // Check repo for name
        return true;
    }
}