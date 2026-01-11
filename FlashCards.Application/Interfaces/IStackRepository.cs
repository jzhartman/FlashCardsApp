using FlashCards.Core.Entities;

namespace FlashCards.Application.Interfaces;

public interface IStackRepository
{
    CardStack GetById(int id);
    List<CardStack> GetAllStacks();
    int Add(string name);
    void DeleteById(int id);
    void Update();
    bool ExistsByName(string name);
}
