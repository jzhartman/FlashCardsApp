using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.UseCases.Decks;

public class GetAllDecksHandler
{
    private readonly IDeckRepository _repo;

    public GetAllDecksHandler(IDeckRepository repo)
    {
        _repo = repo;
    }

    public List<Deck> Handle()
    {
        return _repo.GetAllDecks();
    }
}
