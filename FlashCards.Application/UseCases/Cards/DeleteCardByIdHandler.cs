using FlashCards.Application.Interfaces;

namespace FlashCards.Application.UseCases.Cards;

public class DeleteCardByIdHandler : IDeleteCardByIdHandler
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
