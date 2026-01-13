using FlashCards.Core.Entities;

namespace FlashCards.Application.Interfaces;

public interface IDeckRepository
{
    Deck GetById(int id);
    List<Deck> GetAllDecks();
    int Add(string name);
    void DeleteById(int id);
    void Update();
    bool ExistsByName(string name);
}
