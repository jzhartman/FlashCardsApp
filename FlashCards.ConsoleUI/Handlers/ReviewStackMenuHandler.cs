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
                case "Add Card":
                    HandleAddCard();
                    cards = GetAllCardsInStack(stackName);
                    break;
                case "Edit Card":
                    HandleEditCard();
                    break;
                case "Delete Card":
                    HandleDeleteCard();
                    break;
                case "Return to Previous Menu":
                    return;
                default:
                    AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!");
                    break;
            }
        }
    }

    private List<CardResponse> GetAllCardsInStack(string stackName)
    {
        var handler = _provider.GetRequiredService<GetAllCardsByStackName>();
        return handler.Handle(stackName);
    }

    private void PrintCards()
    {
        int i = 1;
        foreach (var card in CurrentStack.Cards)
        {
            Console.WriteLine($"{i}: {card.FrontText} \t {card.BackText}");
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

        // Will need to get all cards for the stack again...

    }
    private void PressAnyKeyToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }


    //
    // NOT IMPLEMENTED YET
    //

    private void HandleDeleteCard()
    {
        AnsiConsole.MarkupLine("Delete that card...");
        PressAnyKeyToContinue();
    }
    private void HandleEditCard()
    {
        AnsiConsole.MarkupLine("Edit my card...");
        PressAnyKeyToContinue();

    }
}
