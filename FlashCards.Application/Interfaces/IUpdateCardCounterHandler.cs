namespace FlashCards.Application.Interfaces;

public interface IUpdateCardCounterHandler
{
    void Handle(List<int> cardsCorrect, List<int> cardsIncorrect);
}