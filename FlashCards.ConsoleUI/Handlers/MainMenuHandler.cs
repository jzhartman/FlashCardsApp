using FlashCards.ConsoleUI.Handlers;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Controllers;

public class MainMenuHandler
{
    private readonly StackMenuHandler _stackMenu;
    private readonly StudyMenuHandler _studyMenu;

    public MainMenuHandler(StackMenuHandler stackMenu, StudyMenuHandler studyMenu)
    {
        _stackMenu = stackMenu;
        _studyMenu = studyMenu;
    }
    public void Run()
    {
        bool exitApp = false;

        while (exitApp == false)
        {
            var selection = PrintMainMenuAndGetSelection();
            exitApp = HandleUserSelection(selection);
        }
    }

    private string PrintMainMenuAndGetSelection()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[bold green]Main Menu[/]\r\n");

        return AnsiConsole.Prompt(new SelectionPrompt<string>()
                            .Title("Select from the options below:")
                            .AddChoices(new[]
                            {
                                                "Manage Stacks",
                                                "Study",
                                                "View Reports",
                                                "Exit"
                            }));
    }

    private bool HandleUserSelection(string selection)
    {
        switch (selection)
        {
            case "Manage Stacks": _stackMenu.Run(); break;
            case "Study": _studyMenu.Run(); break;
            case "View Reports": HandleReports(); break;
            case "Exit": return true;
            default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
        }

        return false;
    }

    private void HandleReports()
    {
        AnsiConsole.MarkupLine("Reporting for duty, sir!");
    }
}
