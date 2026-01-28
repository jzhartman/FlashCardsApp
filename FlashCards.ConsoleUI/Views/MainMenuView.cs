using FlashCards.ConsoleUI.Enums;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Views;

public class MainMenuView
{
    public MainMenuItem Render()
    {
        return AnsiConsole.Prompt(
                    new SelectionPrompt<MainMenuItem>()
                    .Title("Select from the options below:")
                    .UseConverter(menu => menu switch
                    {
                        MainMenuItem.Review => "Review Cards in Stack",
                        MainMenuItem.Create => "Create New Stack",
                        MainMenuItem.Delete => "Delete Stack",
                        MainMenuItem.Study => "Begin Study Session",
                        MainMenuItem.Past => "View Past Study Sessions",
                        MainMenuItem.Report => "View Reports",
                        MainMenuItem.Exit => "Exit",
                        _ => menu.ToString()
                    })
                    .AddChoices(Enum.GetValues<MainMenuItem>()));
    }
}