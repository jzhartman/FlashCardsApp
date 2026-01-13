using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.UseCases.Decks;

public class GetDeckByIdHandler
{
    private readonly IDeckRepository _repo;

    public GetDeckByIdHandler(IDeckRepository repo)
    {
        _repo = repo;
    }

    public Deck Handle(int id)
    {
        return _repo.GetById(id);
    }
}
