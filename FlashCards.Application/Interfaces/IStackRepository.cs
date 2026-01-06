using FlashCards.Core.Entities;

namespace FlashCards.Application.Interfaces;

public interface IStackRepository
{
    Stack GetById(int id);
    List<Stack> GetAllStacks();
    int Add(string name);
    void DeleteById(int id);
    void Update();
    bool ExistsByName();
}
