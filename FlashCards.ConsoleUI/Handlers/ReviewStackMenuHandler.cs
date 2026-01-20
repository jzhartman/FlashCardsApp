using FlashCards.Application.DTOs;
using FlashCards.Application.UseCases.Cards;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using FlashCards.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Handlers;

public class ReviewStackMenuHandler
{
    private readonly IServiceProvider _provider;
    private readonly IConsoleInput _input;
    private readonly IConsoleOutput _output;

    private StackResponse CurrentStack;

    public ReviewStackMenuHandler(IServiceProvider provider, IConsoleInput input, IConsoleOutput output)
    {
        _provider = provider;
        _input = input;
        _output = output;
    }

    private void SetStack(string stackName, List<CardResponse> cards)
    {
        CurrentStack = new StackResponse(stackName, cards);
    }

    public void Run(string stackName)
    {
        while (true)
        {
            _output.PrintPageTitle("REVIEW STACK MENU");

            var cards = GetAllCardsInStack(stackName);
            SetStack(stackName, cards);
            _output.PrintCards(CurrentStack);

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
    private void HandleAddCard()
    {
        var frontText = _input.GetTextInputFromUser("Enter front text: ");
        var backText = _input.GetTextInputFromUser("Enter back text: ");

        var handler = _provider.GetRequiredService<AddCardHandler>();
        var result = handler.Handle(CurrentStack.Name, frontText, backText);

        if (!result.IsValid) _output.PrintValidationErrorsFromCollection<Card>(result);
        else _output.PrintSuccessMessage($"Added card to {CurrentStack.Name}!");
        _input.PressAnyKeyToContinue();
    }

    private void HandleDeleteCard(List<CardResponse> cards)
    {
        var card = cards[_input.GetRecordIdFromUser("delete", 1, cards.Count) - 1];

        if (_input.GetDeleteCardConfirmationFromUser(card.FrontText, card.BackText))
        {
            var handler = _provider.GetRequiredService<DeleteCardByIdHandler>();
            handler.Handle(card.Id);
            _output.PrintSuccessMessage($"Deleted [yellow]card[/]!");
        }
        else _output.PrintCancellationMessage("deletion", "card");

        _input.PressAnyKeyToContinue();
    }


    //
    // NOT IMPLEMENTED YET
    //

    private void HandleEditCard()
    {
        var originalCard = CurrentStack.Cards[_input.GetRecordIdFromUser("edit", 1, CurrentStack.Cards.Count) - 1];

        var newFrontText = GetEditedFrontTextFromUser(originalCard);
        // Get front text from user:



        AnsiConsole.MarkupLine("Edit my card...");
        _input.PressAnyKeyToContinue();

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
