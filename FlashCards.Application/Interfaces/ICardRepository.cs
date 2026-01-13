using FlashCards.Core.Entities;

namespace FlashCards.Application.Interfaces;

public interface ICardRepository
{
    Card GetById(int id);
    List<Card> GetAllByDeckId(int id);
    int Add(Card card);
    void Delete(int id);
    void Update();
    bool ExistsByFrontText(string text, int deckId);
    bool ExistsByBackText(string text, int deckId);
}
