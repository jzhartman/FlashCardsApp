using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.Stacks.GetById;

public class GetStackById
{
    private readonly IStackRepository _repo;

    public GetStackById(IStackRepository repo)
    {
        _repo = repo;
    }

    public Stack Handle(int id)
    {
        return _repo.GetById(id);
    }
}
