using Spectre.Console;

namespace FlashCards.ConsoleUI.Views;

public class SessionYearSelectionView
{
    public int Render(int[] sessionYear)
    {
        return AnsiConsole.Prompt(
                    new SelectionPrompt<int>()
                    .Title("Select the year to view a report:")
                    .AddChoices(sessionYear));
    }
}
