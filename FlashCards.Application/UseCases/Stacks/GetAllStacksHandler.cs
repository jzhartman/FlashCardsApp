using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.UseCases.Stacks;

public class GetAllStacksHandler
{
    private readonly IStackRepository _repo;

    public GetAllStacksHandler(IStackRepository repo)
    {
        _repo = repo;
    }

    public List<CardStack> Handle()
    {
        return _repo.GetAllStacks();
    }
}
