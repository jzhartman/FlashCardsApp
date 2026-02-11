using FlashCards.Application.Interfaces;

namespace FlashCards.Application.Cards.UpdateCardCounters;

public class UpdateCardCountersHandler
{
    private readonly ICardRepository _repo;

    public UpdateCardCountersHandler(ICardRepository repo)
    {
        _repo = repo;
    }

    public void Handle(List<int> cardsCorrect, List<int> cardsIncorrect)
    {
        foreach (var card in cardsCorrect)
        {
            _repo.UpdateCardCounters(card, 1, 1, 0);
        }
        foreach (var card in cardsIncorrect)
        {
            _repo.UpdateCardCounters(card, 1, 0, 1);
        }
    }
}
