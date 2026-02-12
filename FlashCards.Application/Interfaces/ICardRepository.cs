using FlashCards.Core.Entities;

namespace FlashCards.Application.Interfaces;

public interface ICardRepository
{
    int Add(Card card);
    void DeleteAllByStackId(int id);
    void DeleteById(int id);
    bool ExistsByBackText(string text, int stackId);
    bool ExistsByBackTextExcludingId(string text, int stackId, int cardId);
    bool ExistsByFrontText(string text, int stackId);
    bool ExistsByFrontTextExcludingId(string text, int stackId, int cardId);
    List<Card> GetAllByStackId(int id);
    int GetCardCountByStackId(int id);
    int GetIdByTextAndStackId(int stackId, string frontText, string backText);
    void UpdateCardCounters(int id, int studied, int correct, int incorrect);
    void UpdateCardText(int id, string frontText, string backText);
}