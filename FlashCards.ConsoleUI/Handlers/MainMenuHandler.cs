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
        while (true)
        {
            var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                .Title("Select from the options below:")
                .AddChoices(new[]
                {
                        "Manage Stacks",
                        "Study",
                        "View Reports",
                        "Exit"
                })
            );

            switch (selection)
            {
                case "Manage Stacks": _stackMenu.Run(); break;
                case "Study": _studyMenu.Run(); break;
                case "View Reports": HandleReports(); break;
                case "Exit": return;
                default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
            }
        }
    }

    private void HandleReports()
    {
        AnsiConsole.MarkupLine("Reporting for duty, sir!");
    }
}
