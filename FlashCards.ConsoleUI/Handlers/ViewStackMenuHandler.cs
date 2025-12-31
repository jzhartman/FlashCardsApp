using Spectre.Console;

namespace FlashCards.ConsoleUI.Handlers;

public class ViewStackMenuHandler
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
                        "Add Card",
                        "Edit Card",
                        "Delete Card",
                        "Return to Previous Menu"
                })
            );

            switch (selection)
            {
                case "Add Card": HandleAddCard(); break;
                case "Edit Card": HandleEditCard(); break;
                case "Delete Card": HandleDeleteCard(); break;
                case "Return to Previous Menu": return;
                default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
            }
        }
    }

    private void HandleDeleteCard()
    {
        AnsiConsole.MarkupLine("Delete that card...");
    }

    private void HandleEditCard()
    {
        AnsiConsole.MarkupLine("Edit my card...");

    }

    private void HandleAddCard()
    {
        AnsiConsole.MarkupLine("Let's add a new card!");
    }
}
