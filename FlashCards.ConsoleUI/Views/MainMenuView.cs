using FlashCards.ConsoleUI.Enums;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Views;

public class MainMenuView
{
    public MainMenuItem Render(MainMenuItem[] menuItems)
    {
        return AnsiConsole.Prompt(
                    new SelectionPrompt<MainMenuItem>()
                    .Title("Select from the options below:")
                    .UseConverter(menu => menu switch
                    {
                        MainMenuItem.ReviewStack => "Review Cards in Stack",
                        MainMenuItem.CreateStack => "Create New Stack",
                        MainMenuItem.DeleteStack => "Delete Stack",
                        MainMenuItem.StudyStack => "Begin Study Session",
                        MainMenuItem.ViewPastSessions => "View Past Study Sessions",
                        MainMenuItem.Report => "View Reports",
                        MainMenuItem.Exit => "Exit",
                        _ => menu.ToString()
                    })
                    .AddChoices(menuItems));
    }
}