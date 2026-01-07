using FlashCards.Application.Interfaces;

namespace FlashCards.Application.Services;

public class CardUniquenessService
{
    private readonly ICardRepository _repo;

    public CardUniquenessService(ICardRepository repo)
    {
        _repo = repo;
    }

    public bool IsCardUnique(string frontText, string backText, int stackId)
    {
        bool cardExists = false;

        if (_repo.ExistsByFrontText(frontText, stackId) && _repo.ExistsByBackText(backText, stackId))
            cardExists = true;

        return !cardExists;
    }
}
