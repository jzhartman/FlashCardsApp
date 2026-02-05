using FlashCards.Application.Interfaces;

namespace FlashCards.Application.Cards.Delete;

public class DeleteCardByIdHandler
{
    private readonly ICardRepository _repo;

    public DeleteCardByIdHandler(ICardRepository repo)
    {
        _repo = repo;
    }

    public void Handle(int id)
    {
        _repo.DeleteById(id);
    }
}
