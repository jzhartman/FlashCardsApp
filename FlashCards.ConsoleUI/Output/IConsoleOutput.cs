using FlashCards.Application.DTOs;
using FlashCards.Core.Validation;

namespace FlashCards.ConsoleUI.Output;

public interface IConsoleOutput
{
    void PrintCancellationMessage(string action, string item);
    void PrintCard(CardResponse card, int i);
    void PrintCards(StackResponse stack);
    void PrintPageTitle(string title);
    void PrintStackList(List<StackNameAndCardCountResponse> stacks);
    void PrintSuccessMessage(string message);
    void PrintValidationErrorsFromCollection(List<Error> errors);
}
