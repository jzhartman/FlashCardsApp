using FlashCards.Core.Entities;

namespace FlashCards.Application.Interfaces;

public interface ICardRepository
{
    Card GetById(int id);
    List<Card> GetAllByStackId(int id);
    int Add(Card card);
    void Delete(int id);
    void Update();
    bool ExistsByFrontText(string text, int stackId);
    bool ExistsByBackText(string text, int stackId);
}
