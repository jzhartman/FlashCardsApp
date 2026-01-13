using FlashCards.ConsoleUI.Handlers;
using FlashCards.Core.Entities;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Controllers;

public class MainMenuHandler
{
    private readonly DeckMenuHandler _deckMenu;
    private readonly StudyMenuHandler _studyMenu;

    public MainMenuHandler(DeckMenuHandler deckMenu, StudyMenuHandler studyMenu)
    {
        _deckMenu = deckMenu;
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
                                                "Manage Decks",
                                                "Study",
                                                "View Reports",
                                                "Exit"
                            }));
    }

    private bool HandleUserSelection(string selection)
    {
        switch (selection)
        {
            case "Manage Decks": _deckMenu.Run(); break;
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

    private void PrintDeckList(List<Deck> decks)
    {
        if (decks.Count == 0)
            AnsiConsole.MarkupLine("No decks exist!");
        else
        {
            int i = 1;

            Console.WriteLine($"ID  NAME\tCARD COUNT");
            foreach (var deck in decks)
            {
                var cardCount = (deck.Cards != null) ? deck.Cards.Count : 0;
                AnsiConsole.MarkupLine($"{i}: {deck.Name}\t{cardCount}");
                i++;
            }
        }
        Console.WriteLine();
    }
}
