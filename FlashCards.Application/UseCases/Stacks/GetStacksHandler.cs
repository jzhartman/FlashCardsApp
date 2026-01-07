using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.UseCases.Stacks;

public class GetStacksHandler
{
    private readonly IStackRepository _repo;

    public GetStacksHandler(IStackRepository repo)
    {
        _repo = repo;
    }

    public List<Stack> HandleGetAll()
    {
        return _repo.GetAllStacks();
    }

    public Stack HandleGetById(int id)
    {
        return _repo.GetById(id);
    }
}
