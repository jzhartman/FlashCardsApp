using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.UseCases.Stacks;

public class GetStackById
{
    private readonly IStackRepository _repo;

    public GetStackById(IStackRepository repo)
    {
        _repo = repo;
    }

    public CardStack Handle(int id)
    {
        return _repo.GetById(id);
    }
}
