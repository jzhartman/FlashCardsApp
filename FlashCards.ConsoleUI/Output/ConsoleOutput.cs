using FlashCards.Application.Cards;
using FlashCards.Application.DTOs;
using FlashCards.Application.Enums;
using FlashCards.Core.Validation;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Output;

public class ConsoleOutput : IConsoleOutput
{
    public void PrintAppTitle()
    {
        var figlet = new FigletText("FLASH CARDS v1")
            .Color(Color.Blue);
        AnsiConsole.Write(figlet);
    }
    public void PrintPageTitle(string title)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"[bold green]{title.ToUpper()}[/]\r\n");
    }

    public void PrintCardTextInPanel(string text)
    {
        var panel = new Panel(new Align(new Markup(text), HorizontalAlignment.Center, VerticalAlignment.Middle)) { Width = 36, Height = 10 };
        AnsiConsole.Write(panel);
    }
    public void PrintCardTextInSideBySidePanels(string frontText, string backText)
    {
        var frontPanel = new Panel(new Align(new Markup(frontText), HorizontalAlignment.Center, VerticalAlignment.Middle)) { Width = 36, Height = 10 };
        var backPanel = new Panel(new Align(new Markup(backText), HorizontalAlignment.Center, VerticalAlignment.Middle)) { Width = 36, Height = 10 };
        var columns = new Columns(frontPanel, backPanel).Collapse();
        AnsiConsole.Write(columns);
    }
    public void PrintCard(CardResponse card, int i)
    {
        Console.WriteLine($"{i}: {card.FrontText} \t {card.BackText}");
    }
    public void PrintCards(StackResponse stack)
    {
        int i = 1;
        foreach (var card in stack.Cards)
        {
            PrintCard(card, i);
            i++;
        }
        Console.WriteLine();
    }

    public void PrintValidationErrorsFromCollection(List<Error> errors)
    {
        foreach (var error in errors)
        {
            AnsiConsole.MarkupLine($"[red]ERROR:[/] {error.Description}");
        }
    }

    public void PrintSuccessMessage(string message) => AnsiConsole.MarkupLine($"[green]SUCCESS:[/] {message}");
    public void PrintCancellationMessage(string action, string item) => AnsiConsole.MarkupLine($"[yellow]CANCELLED:[/] {action} of {item}!");
    public void PrintNoEditsMadeMessage() => AnsiConsole.MarkupLine($"\r\n[yellow]No changes made to card![/]");

    public void PrintReviewCardsKeypressOptions(CardSide side, int index, int cardCount)
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
}
