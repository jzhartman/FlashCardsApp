using FlashCards.Core.Entities;

namespace FlashCards.Application.Interfaces;

public interface IStackRepository
{
    Stack GetById(int id);
    List<Stack> GetAll();
    int Add(string name);
    void DeleteById(int id);
    void Update();
    bool ExistsByName(string name);
    List<string> GetAllNames();
    int GetIdByName(string name);
    void DeleteByName(string name);
}
