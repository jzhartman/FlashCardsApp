namespace FlashCards.Application.Interfaces;

public interface IDeleteCardByIdHandler
{
    void Handle(int id);
}