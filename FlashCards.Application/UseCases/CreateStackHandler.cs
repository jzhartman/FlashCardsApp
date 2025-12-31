using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.UseCases;

internal class CreateStackHandler
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
        var stack = new Stack() { Name = name };
        return _repo.Add(stack);
    }

    private bool StackNameIsUnique()
    {
        // Check repo for name
        return true;
    }
}