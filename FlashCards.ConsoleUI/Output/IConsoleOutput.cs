using FlashCards.Application.DTOs;
using FlashCards.Core.Entities;
using FlashCards.Core.Validation;

namespace FlashCards.ConsoleUI.Output;

public interface IConsoleOutput
{
    void PrintCancellationMessage(string action, string item);
    void PrintPageTitle(string title);
    void PrintStackList(List<StackNameAndCardCountResponse> stacks);
    void PrintSuccessMessage(string message);
    void PrintValidationErrorsFromCollection(Result<Stack> result);
}
