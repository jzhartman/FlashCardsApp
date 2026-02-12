using FlashCards.Core.Entities;

namespace FlashCards.Application.Interfaces;

public interface IStackRepository
{
    Stack GetById(int id);
    List<Stack> GetAll();
    int Add(string name);
    void DeleteById(int id);
    bool ExistsByName(string name);
    int GetIdByName(string name);
    bool ExistsById(int id);
}
