using FlashCards.Application.Cards;
using FlashCards.Application.Enums;
using FlashCards.Core.Validation;

namespace FlashCards.ConsoleUI.Output;

public interface IConsoleOutput
{
    void PrintAppTitle();
    void PrintCancellationMessage(string action, string item);
    void PrintCard(CardResponse card, int i);
    void PrintCardTextInPanel(string text);
    void PrintCardTextInSideBySidePanels(string frontText, string backText);
    void PrintNoEditsMadeMessage();
    void PrintPageTitle(string title);
    void PrintReviewCardsKeypressOptions(CardSide side, int index, int cardCount);
    void PrintSuccessMessage(string message);
    void PrintValidationErrorsFromCollection(List<Error> errors);
}
