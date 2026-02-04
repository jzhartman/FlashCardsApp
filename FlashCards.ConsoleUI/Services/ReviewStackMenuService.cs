using FlashCards.Application.DTOs;
using FlashCards.Application.Enums;
using FlashCards.Application.Interfaces;
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
    private readonly CardListView _cardListView;

    private readonly IGetAllCardsByStackName _getAllCardsByName;
    private readonly IAddCardHandler _addCard;
    private readonly IGetCardTextHandler _getCardText;
    private readonly IEditCardTextHandler _editCardFrontText;
    private readonly IEditCardHandler _editCard;
    private readonly IDeleteCardByIdHandler _deleteCardById;

    private StackResponse CurrentStack;

    public ReviewStackMenuService(IConsoleInput input, IConsoleOutput output, ReviewStackMenuView menu, CardListView cardListView,
                                    IGetAllCardsByStackName getAllCardsByName, IGetCardTextHandler getCardText, IAddCardHandler addCard,
                                    IEditCardTextHandler editCardFrontText, IEditCardHandler editCard, IDeleteCardByIdHandler deleteCardById)
    {
        _input = input;
        _output = output;
        _menu = menu;
        _cardListView = cardListView;

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

    public void Run(StackNameAndCardCountResponse stack)
    {
        while (true)
        {
            _output.PrintPageTitle($"REVIEWING STACK: {stack.Name}");

            var cards = GetCards(stack.Name);
            _cardListView.Render(cards);

            ReviewStackMenuItem[] menuItems = Enum.GetValues<ReviewStackMenuItem>();

            if (cards.Count <= 0) menuItems = new ReviewStackMenuItem[2] { ReviewStackMenuItem.AddCard, ReviewStackMenuItem.Return };

            SetStack(stack.Name, cards);

            var selection = _menu.Render(menuItems);

            switch (selection)
            {
                case ReviewStackMenuItem.ReviewCards: HandleReviewCards(cards); break;
                case ReviewStackMenuItem.AddCard: HandleAddCard(); break;
                case ReviewStackMenuItem.EditCard: HandleEditCard(); break;
                case ReviewStackMenuItem.DeleteCard: HandleDeleteCard(cards); break;
                case ReviewStackMenuItem.Return: return;
                default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
            }
            _input.PressAnyKeyToContinue(2);
        }
    }

    private List<CardResponse> GetCards(string stackName)
    {
        var result = _getAllCardsByName.Handle(stackName);

        if (result.IsFailure)
        {
            _output.PrintValidationErrorsFromCollection(result.Errors);
            return new List<CardResponse>();
        }
        else
        {
            return result.Value;
        }
    }

    private void HandleReviewCards(List<CardResponse> cards)
    {
        var shuffleableCards = cards;
        bool continueReview = true;
        ConsoleKeyInfo keyInfo;
        int i = 0;

        while (continueReview)
        {
            _output.PrintPageTitle("REVIEW STACK MENU");

            PrintCardTextInPanel(shuffleableCards[i].FrontText);
            PressAnyKeyToFlipCard();

            _output.PrintPageTitle("REVIEW STACK MENU");
            PrintSideBySidePanels(shuffleableCards[i].FrontText, shuffleableCards[i].BackText);

            PrintOptions(CardSide.Back, i, shuffleableCards.Count);


            bool validKey = false;

            while (validKey == false)
            {
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (i > 0) i--;
                        else i = shuffleableCards.Count - 1;
                        validKey = true;
                        break;
                    case ConsoleKey.RightArrow:
                        if (i < shuffleableCards.Count - 1) i++;
                        else i = 0;
                        validKey = true;
                        break;
                    case ConsoleKey.UpArrow:
                        shuffleableCards = ShuffleStack(shuffleableCards);
                        i = 0;
                        validKey = true;
                        break;
                    case ConsoleKey.Escape:
                        validKey = true;
                        continueReview = false;
                        break;
                    default:
                        Console.WriteLine($"Invalid keypress -- see options");
                        break;
                }
            }
        }
    }

    public void PressAnyKeyToFlipCard()
    {
        Console.Write("Press any key to flip card...");
        Console.ReadKey();
    }

    private List<CardResponse> ShuffleStack(List<CardResponse> cards)
    {
        CardResponse[] cardsArray = cards.ToArray();
        Random.Shared.Shuffle(cardsArray);
        return cardsArray.ToList();
    }

    private void PrintOptions(CardSide side, int index, int cardCount)
    {
        var table = new Table()
                        .RoundedBorder()
                        .BorderColor(Color.Blue)
                        .ShowRowSeparators();

        table.AddColumn("Control");
        table.AddColumn("Key");

        if (side != CardSide.Front && index > 0) table.AddRow("Previous Card", "Left Arrow");
        if (side != CardSide.Front && index < cardCount) table.AddRow("Next Card", "Right Arrow");
        table.AddRow("Shuffle Deck", "Up Arrow");
        table.AddRow("Return to Menu", "Esc");


        AnsiConsole.Write(table);

        Console.WriteLine();
    }

    public void PrintCardTextInPanel(string text)
    {
        var panel = new Panel(new Align(new Markup(text), HorizontalAlignment.Center, VerticalAlignment.Middle)) { Width = 30, Height = 10 };
        AnsiConsole.Write(panel);
    }

    public void PrintSideBySidePanels(string frontText, string backText)
    {
        var frontPanel = new Panel(new Align(new Markup(frontText), HorizontalAlignment.Center, VerticalAlignment.Middle)) { Width = 30, Height = 10 };
        var backPanel = new Panel(new Align(new Markup(backText), HorizontalAlignment.Center, VerticalAlignment.Middle)) { Width = 30, Height = 10 };
        var columns = new Columns(frontPanel, backPanel).Collapse();
        AnsiConsole.Write(columns);

    }

    private void HandleAddCard()
    {
        var card = new AddCardCommand(CurrentStack.Name,
                                        GetCardText(CardSide.Front),
                                        GetCardText(CardSide.Back));

        var result = _addCard.Handle(card);

        if (result.IsSuccess) _output.PrintSuccessMessage($"Added card to {CurrentStack.Name}!");
        else Console.WriteLine("ERROR MESSAGE");
    }

    private string GetCardText(CardSide cardSide)
    {
        bool textValid = false;
        var output = string.Empty;

        while (textValid == false)
        {
            var cardData = new CardTextBySideCommand(CurrentStack.Name,
                                                    _input.GetTextInputFromUser($"Enter {cardSide} text"),
                                                    cardSide);

            var result = _getCardText.Handle(cardData);

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
    }


    private void HandleEditCard()
    {
        var message = "Please enter the [yellow]ID[/] of the card you wish to edit:";
        var originalCard = CurrentStack.Cards[_input.GetRecordIdFromUser(message, 1, CurrentStack.Cards.Count) - 1];

        var editedCard = new EditCardCommand(CurrentStack.Name,
            GetEditedTextFromUser(originalCard, CardSide.Front),
            GetEditedTextFromUser(originalCard, CardSide.Back));


        if ((originalCard.FrontText == editedCard.FrontText) && (originalCard.BackText == editedCard.BackText))
        {
            _output.PrintNoEditsMadeMessage();
            return;
        }

        bool confirmEdit = _input.GetEditCardConfirmationFromUser(originalCard.FrontText, originalCard.BackText, editedCard.FrontText, editedCard.BackText);

        if (confirmEdit)
        {
            _editCard.Handle(originalCard, editedCard);
            _output.PrintSuccessMessage("Edited card data!");
        }
        else _output.PrintCancellationMessage("editing", "card text");
    }
    private string GetEditedTextFromUser(CardResponse card, CardSide cardSide)
    {
        bool textValid = false;
        var textInput = string.Empty;

        var currentCardText = (cardSide == CardSide.Front) ? $"{card.FrontText}" : $"{card.BackText}";
        var promptText = $"Original Card {cardSide} Text: [green]{currentCardText}[/]\r\nEnter new text or leave blank to keep original";

        while (textValid == false)
        {
            textInput = _input.GetTextInputFromUser(promptText, 1);

            var editedCardSide = new CardTextBySideCommand(CurrentStack.Name,
                                                          textInput,
                                                          cardSide);

            var result = _editCardFrontText.Handle(card, editedCardSide);

            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors)
                {
                    AnsiConsole.WriteLine(error.Description);
                }
            }
            textValid = result.IsSuccess;
            textInput = result.Value;
        }
        return textInput;
    }
}
