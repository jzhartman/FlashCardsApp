using FlashCards.Application.DTOs;
using FlashCards.Application.UseCases.Cards;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
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
        var frontText = GetCardText("front");
        var backText = GetCardText("back");

        var handler = _provider.GetRequiredService<AddCardHandler>();
        var result = handler.Handle(CurrentStack.Name, frontText, backText);

        _output.PrintSuccessMessage($"Added card to {CurrentStack.Name}!");

        _input.PressAnyKeyToContinue();
    }

    private string GetCardText(string cardSide)
    {
        bool textValid = false;
        var output = string.Empty;

        while (textValid == false)
        {
            var text = _input.GetTextInputFromUser($"Enter {cardSide} text");

            var handler = _provider.GetRequiredService<GetCardFrontTextHandler>();
            var result = handler.Handle(CurrentStack.Name, text, cardSide);

            if (result.IsFailure) _output.PrintValidationErrorsFromCollection(result.Errors);

            else
            {
                output = result.Value;
                textValid = true;
            }
        }
        return output;
    }




    private void HandleDeleteCard(List<CardResponse> cards)
    {
        var card = cards[_input.GetRecordIdFromUser("delete", 1, cards.Count) - 1];

        if (_input.GetDeleteCardConfirmationFromUser(card.FrontText, card.BackText))
        {
            var handler = _provider.GetRequiredService<DeleteCardByIdHandler>();
            handler.Handle(card.Id);
            _output.PrintSuccessMessage($"Deleted [yellow]{card.FrontText}[/] card!");
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

        var newFrontText = GetEditedTextFromUser(originalCard, "Front");
        var newBackText = GetEditedTextFromUser(originalCard, "Back");

        if ((originalCard.FrontText == newFrontText) && (originalCard.BackText == newBackText))
        {
            _output.PrintNoEditsMadeMessage();
            _input.PressAnyKeyToContinue(1);
            return;
        }

        bool confirmEdit = _input.GetEditCardConfirmationFromUser(originalCard.FrontText, originalCard.BackText, newFrontText, newBackText);

        if (confirmEdit)
        {
            var handler = _provider.GetRequiredService<EditCardHandler>();
            handler.Handle(CurrentStack.Name, originalCard, newFrontText, newBackText);
            _output.PrintSuccessMessage("Edited card data!");
        }
        else _output.PrintCancellationMessage("editing", "card text");

        _input.PressAnyKeyToContinue();

    }
    private string GetEditedTextFromUser(CardResponse card, string cardSide)
    {
        bool textValid = false;
        var input = string.Empty;

        var promptText = (cardSide.ToUpper() == "FRONT") ? $"{card.FrontText}" : $"{card.BackText}";

        while (textValid == false)
        {
            input = _input.GetTextInputFromUser($"Original Card {cardSide} Text: [green]{promptText}[/]\r\nEnter new text or leave blank to keep original", 1);

            var handler = _provider.GetRequiredService<EditCardFrontTextHandler>();
            var result = handler.Handle(card, input, CurrentStack.Name, cardSide);

            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors)
                {
                    AnsiConsole.WriteLine(error.Description);
                }
            }
            textValid = result.IsSuccess;
            input = result.Value;
        }
        return input;
    }
}
