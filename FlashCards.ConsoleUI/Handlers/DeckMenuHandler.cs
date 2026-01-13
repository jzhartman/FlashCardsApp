using FlashCards.Application.UseCases.Decks;
using FlashCards.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Handlers;

public class DeckMenuHandler
{
    private readonly IServiceProvider _provider;
    private readonly ViewDeckMenuHandler _viewDeckMenu;

    public DeckMenuHandler(IServiceProvider provider, ViewDeckMenuHandler viewDeckMenu)
    {
        _provider = provider;
        _viewDeckMenu = viewDeckMenu;
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold green]Manage Deck Menu[/]\r\n");

            var decks = GetAllDecks();
            PrintDeckList(decks);

            var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                .Title("Select from the options below:")
                .AddChoices(new[]
                {
                        "View Deck",
                        "Add Deck",
                        "Delete Deck",
                        "Return to Main Menu"
                })
            );

            switch (selection)
            {
                case "View Deck":
                    var deck = GetDeckSelectionFromUser(decks, "view");
                    HandleViewDeck(deck);
                    break;
                case "Add Deck": HandleAddDeck(); break;
                case "Delete Deck": HandleDeleteDeck(); break;
                case "Return to Main Menu": return;
                default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
            }
        }
    }

    private void HandleViewDeck(Deck deck)
    {
        AnsiConsole.MarkupLine($"Viewing deck {deck.Name}!");
        _viewDeckMenu.Run(deck);
    }

    private Deck GetDeckSelectionFromUser(List<Deck> decks, string action)
    {
        AnsiConsole.Write($"Enter ID of the deck you wish to {action}: ");
        int id = Int32.Parse(Console.ReadLine());
        return decks[id - 1];
    }

    private void HandleDeleteDeck()
    {
        AnsiConsole.MarkupLine("Handling the delete...");
    }

    private void HandleAddDeck()
    {
        var input = GetNameFromUser();
        var handler = _provider.GetRequiredService<AddDeckHandler>();
        var result = handler.Handle(input);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                AnsiConsole.WriteLine(error);
            }
        }

        else AnsiConsole.WriteLine($"Added deck {result.Value.Name}!");
    }

    private List<Deck> GetAllDecks()
    {
        var handler = _provider.GetRequiredService<GetAllDecksHandler>();
        return handler.Handle();
    }

    private string GetNameFromUser()
    {
        AnsiConsole.Markup("Enter deck name: ");
        return Console.ReadLine();
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
