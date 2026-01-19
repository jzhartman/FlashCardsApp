using FlashCards.Application.DTOs;
using FlashCards.Application.UseCases.Cards;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Handlers;

public class ReviewStackMenuHandler
{
    private readonly IServiceProvider _provider;

    private StackResponse CurrentStack;

    public ReviewStackMenuHandler(IServiceProvider provider)
    {
        _provider = provider;
    }

    private void SetStack(string stackName, List<CardResponse> cards)
    {
        CurrentStack = new StackResponse(stackName, cards);
    }

    public void Run(string stackName)
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold green]Review Stack Menu[/]\r\n");

            var cards = GetAllCardsInStack(stackName);
            SetStack(stackName, cards);
            PrintCards();

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
                case "Delete Card": HandleDeleteCard(cards); break;
                case "Return to Previous Menu": return;
                default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
            }
        }
    }

    private List<CardResponse> GetAllCardsInStack(string stackName)
    {
        var handler = _provider.GetRequiredService<GetAllCardsByStackName>();
        return handler.Handle(stackName);
    }

    private void PrintCard(CardResponse card, int i)
    {
        Console.WriteLine($"{i}: {card.FrontText} \t {card.BackText}");
    }
    private void PrintCards()
    {
        int i = 1;
        foreach (var card in CurrentStack.Cards)
        {
            PrintCard(card, i);
            i++;
        }
        Console.WriteLine();
    }



    private void HandleAddCard()
    {
        AnsiConsole.Write("Enter front text: ");
        var frontText = Console.ReadLine();
        AnsiConsole.Write("Enter back text: ");
        var backText = Console.ReadLine();


        var handler = _provider.GetRequiredService<AddCardHandler>();
        var result = handler.Handle(CurrentStack.Name, frontText, backText);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                AnsiConsole.WriteLine(error);
            }
        }

        else AnsiConsole.WriteLine($"Added card to {CurrentStack.Name}!");
        PressAnyKeyToContinue();
    }
    private void PressAnyKeyToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private bool ConfirmDelete(CardResponse card)
    {
        Console.WriteLine("About to delete card with the following data:");
        Console.WriteLine($"Front Text:\t{card.FrontText}");
        Console.WriteLine($"Back Text:\t{card.BackText}");
        Console.WriteLine();
        Console.Write("Enter y to delete or anything else to cancel: ");
        var input = Console.ReadLine();

        return input == "y" ? true : false;
    }

    private void HandleDeleteCard(List<CardResponse> cards)
    {
        var card = GetCardSelectionFromUser(cards, "delete");

        if (ConfirmDelete(card))
        {
            var handler = _provider.GetRequiredService<DeleteCardByIdHandler>();
            handler.Handle(card.Id);
            Console.WriteLine("Card deleted!");
        }
        else Console.WriteLine("Cancelled delete!");

        PressAnyKeyToContinue();
    }

    private CardResponse GetCardSelectionFromUser(List<CardResponse> cards, string action)
    {
        AnsiConsole.Write($"Enter ID of the card you wish to {action}: ");
        int id = Int32.Parse(Console.ReadLine());
        return cards[id - 1];
    }



    //
    // NOT IMPLEMENTED YET
    //

    private void HandleEditCard()
    {
        var originalCard = GetCardSelectionFromUser(CurrentStack.Cards, "review");
        var newFrontText = GetEditedFrontTextFromUser(originalCard);
        // Get front text from user:



        AnsiConsole.MarkupLine("Edit my card...");
        PressAnyKeyToContinue();

    }

    private string GetEditedFrontTextFromUser(CardResponse card)
    {
        bool textValid = false;
        var input = string.Empty;

        while (textValid == false)
        {
            Console.WriteLine($"Original Card Front Text: {card.FrontText}");
            Console.Write("Enter new text or leave blank to keep original: ");
            input = Console.ReadLine();

            var handler = _provider.GetRequiredService<EditCardFrontTextHandler>();
            var result = handler.Handle(card, input, CurrentStack.Name);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    AnsiConsole.WriteLine(error);
                }
            }

            textValid = result.IsValid;
        }

        return input;

    }
}
