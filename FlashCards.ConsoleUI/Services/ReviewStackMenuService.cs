using FlashCards.Application.DTOs;
using FlashCards.Application.Enums;
using FlashCards.Application.UseCases.Cards;
using FlashCards.ConsoleUI.Enums;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using FlashCards.ConsoleUI.Views;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Handlers;

public class ReviewStackMenuService
{
    private readonly IConsoleInput _input;
    private readonly IConsoleOutput _output;

    private readonly ReviewStackMenuView _menu;

    private readonly IGetAllCardsByStackName _getAllCardsByName;
    private readonly IAddCardHandler _addCard;
    private readonly IGetCardTextHandler _getCardText;
    private readonly IEditCardFrontTextHandler _editCardFrontText;
    private readonly IEditCardHandler _editCard;
    private readonly IDeleteCardByIdHandler _deleteCardById;

    private StackResponse CurrentStack;

    public ReviewStackMenuService(IConsoleInput input, IConsoleOutput output, ReviewStackMenuView menu,
                                    IGetAllCardsByStackName getAllCardsByName, IGetCardTextHandler getCardText, IAddCardHandler addCard,
                                    IEditCardFrontTextHandler editCardFrontText, IEditCardHandler editCard, IDeleteCardByIdHandler deleteCardById)
    {
        _input = input;
        _output = output;
        _menu = menu;

        _getAllCardsByName = getAllCardsByName;
        _getCardText = getCardText;
        _addCard = addCard;
        _editCardFrontText = editCardFrontText;
        _editCard = editCard;
        _deleteCardById = deleteCardById;
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

            var cards = _getAllCardsByName.Handle(stackName);
            SetStack(stackName, cards);
            _output.PrintCards(CurrentStack);

            var selection = _menu.Render();

            switch (selection)
            {
                case ReviewStackMenuItem.AddCard: HandleAddCard(); break;
                case ReviewStackMenuItem.EditCard: HandleEditCard(); break;
                case ReviewStackMenuItem.DeleteCard: HandleDeleteCard(cards); break;
                case ReviewStackMenuItem.Return: return;
                default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
            }
        }
    }

    private void HandleAddCard()
    {
        var frontText = GetCardText(CardSide.Front);
        var backText = GetCardText(CardSide.Back);

        var result = _addCard.Handle(CurrentStack.Name, frontText, backText);

        _output.PrintSuccessMessage($"Added card to {CurrentStack.Name}!");

        _input.PressAnyKeyToContinue();
    }

    private string GetCardText(CardSide cardSide)
    {
        bool textValid = false;
        var output = string.Empty;

        while (textValid == false)
        {
            var text = _input.GetTextInputFromUser($"Enter {cardSide} text");

            var result = _getCardText.Handle(CurrentStack.Name, text, cardSide);

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
        var message = "Please enter the [yellow]ID[/] of the card you wish to delete:";
        var card = cards[_input.GetRecordIdFromUser(message, 1, cards.Count) - 1];

        if (_input.GetDeleteCardConfirmationFromUser(card.FrontText, card.BackText))
        {
            _deleteCardById.Handle(card.Id);
            _output.PrintSuccessMessage($"Deleted [yellow]{card.FrontText}[/] card!");
        }
        else _output.PrintCancellationMessage("deletion", "card");

        _input.PressAnyKeyToContinue();
    }


    private void HandleEditCard()
    {
        var message = "Please enter the [yellow]ID[/] of the card you wish to edit:";
        var originalCard = CurrentStack.Cards[_input.GetRecordIdFromUser(message, 1, CurrentStack.Cards.Count) - 1];

        var newFrontText = GetEditedTextFromUser(originalCard, CardSide.Front);
        var newBackText = GetEditedTextFromUser(originalCard, CardSide.Back);

        if ((originalCard.FrontText == newFrontText) && (originalCard.BackText == newBackText))
        {
            _output.PrintNoEditsMadeMessage();
            _input.PressAnyKeyToContinue(1);
            return;
        }

        bool confirmEdit = _input.GetEditCardConfirmationFromUser(originalCard.FrontText, originalCard.BackText, newFrontText, newBackText);

        if (confirmEdit)
        {
            _editCard.Handle(CurrentStack.Name, originalCard, newFrontText, newBackText);
            _output.PrintSuccessMessage("Edited card data!");
        }
        else _output.PrintCancellationMessage("editing", "card text");

        _input.PressAnyKeyToContinue();

    }
    private string GetEditedTextFromUser(CardResponse card, CardSide cardSide)
    {
        bool textValid = false;
        var input = string.Empty;

        var promptText = (cardSide == CardSide.Front) ? $"{card.FrontText}" : $"{card.BackText}";

        while (textValid == false)
        {
            input = _input.GetTextInputFromUser($"Original Card {cardSide} Text: [green]{promptText}[/]\r\nEnter new text or leave blank to keep original", 1);

            var result = _editCardFrontText.Handle(card, input, CurrentStack.Name, cardSide);

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
