namespace FlashCards.Application.UseCases.Cards;

public interface IDeleteCardByIdHandler
{
    void Handle(int id);
}