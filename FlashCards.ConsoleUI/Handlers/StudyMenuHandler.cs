using Spectre.Console;

namespace FlashCards.ConsoleUI.Controllers;

public class StudyMenuHandler
{
    public void Run()
    {
        while (true)
        {
            var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                .Title("Select from the options below:")
                .AddChoices(new[]
                {
                        "Study Stack",
                        "View Past Sessions",
                        "Return to Main Menu"
                })
            );

            switch (selection)
            {
                case "Study Stack": HandleStudy(); break;
                case "View Past Sessions": HandleViewPastSessions(); break;
                case "Return to Main Menu": return;
                default: AnsiConsole.MarkupLine("[bold red]ERROR:[/] Invalid input!"); break;
            }
        }
    }

    private void HandleViewPastSessions()
    {
        AnsiConsole.MarkupLine("Look at all these sessions!");
    }

    private void HandleStudy()
    {
        AnsiConsole.MarkupLine("Uh-oh- study time...");
    }
}