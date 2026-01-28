
namespace FlashCards.Application.UseCases.Cards;

public interface IUpdateCardCounterHandler
{
    void Handle(List<int> cardsCorrect, List<int> cardsIncorrect);
}