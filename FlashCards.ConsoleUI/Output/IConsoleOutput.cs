using FlashCards.Application.DTOs;

namespace FlashCards.ConsoleUI.Output;

public interface IConsoleOutput
{
    void PrintPageTitle(string title);
    void PrintStackList(List<StackNameAndCardCountResponse> stacks);
}
