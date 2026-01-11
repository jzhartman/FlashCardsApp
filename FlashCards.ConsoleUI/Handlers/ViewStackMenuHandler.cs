using FlashCards.Application.UseCases.Cards;
using FlashCards.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Handlers;

public class ViewStackMenuHandler
{
    private readonly IServiceProvider _provider;

    private CardStack CurrentStack;

    public ViewStackMenuHandler(IServiceProvider provider)
    {
        _provider = provider;
    }

    private void SetStack(CardStack stack)
    {
        CurrentStack = stack;
    }

    public void Run(CardStack stack)
    {
        SetStack(stack);

        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold green]View Stack Menu[/]\r\n");
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
                case "Delete Card": HandleDeleteCard(); break;
                case "Return to Previous Menu": return;
                default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
            }
        }
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
        AnsiConsole.Write("Enter front text: ");
        var frontText = Console.ReadLine();
        AnsiConsole.Write("Enter back text: ");
        var backText = Console.ReadLine();


        var handler = _provider.GetRequiredService<AddCardHandler>();
        var result = handler.HandleAdd(CurrentStack.Id, frontText, backText);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                AnsiConsole.WriteLine(error);
            }
        }

        else AnsiConsole.WriteLine($"Added card to {CurrentStack.Name}!");


    }
}
