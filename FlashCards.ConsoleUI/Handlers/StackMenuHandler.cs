using Spectre.Console;

namespace FlashCards.ConsoleUI.Handlers;

public class StackMenuHandler
{
    private readonly ViewStackMenuHandler _viewStackMenu;

    public StackMenuHandler(ViewStackMenuHandler viewStackMenu)
    {
        _viewStackMenu = viewStackMenu;
    }

    public void Run()
    {
        while (true)
        {
            PrintStackList();

            var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                .Title("Select from the options below:")
                .AddChoices(new[]
                {
                        "View Stack",
                        "Add Stack",
                        "Delete Stack",
                        "Return to Main Menu"
                })
            );

            switch (selection)
            {
                case "View Stack": HandleViewStack(); break;
                case "Add Stack": HandleAddStack(); break;
                case "Delete Stack": HandleDeleteStack(); break;
                case "Return to Main Menu": return;
                default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
            }
        }
    }

    private void HandleViewStack()
    {
        AnsiConsole.MarkupLine("Oh but which stack to view...?");
        _viewStackMenu.Run();
    }

    private void HandleDeleteStack()
    {
        AnsiConsole.MarkupLine("Handling the delete...");
    }

    private void HandleAddStack()
    {
        AnsiConsole.MarkupLine("Handling the add...");
    }

    private void PrintStackList()
    {
        AnsiConsole.MarkupLine("This is a printed list of all the stacks....");
    }
}
