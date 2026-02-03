using FlashCards.Application.DTOs;
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
        var panel = new Panel(new Align(new Markup(text), HorizontalAlignment.Center, VerticalAlignment.Middle)) { Width = 30, Height = 10 };
        AnsiConsole.Write(panel);
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
    public void PrintNoEditsMadeMessage() => AnsiConsole.MarkupLine($"[yellow]No changes made to card![/]");





    public void PrintSessionResults(StudySessionResponse session)
    {
        var sessions = new List<StudySessionResponse> { session };

        PrintResultsForAllSessions(sessions);
    }
    public void PrintResultsForAllSessions(List<StudySessionResponse> sessions)
    {
        var table = new Table();

        table.AddColumn("Stack Name");
        table.AddColumn("Score");
        table.AddColumn("Time");
        table.AddColumn("# Cards Studied");
        table.AddColumn("# Correct");
        table.AddColumn("# Incorrect");

        foreach (var session in sessions)
        {
            table.AddRow(session.StackName, session.Score.ToString("F1"), session.Time.ToString("yyyy-MM-dd HH:mm"), session.CountStudied.ToString(),
            session.CountCorrect.ToString(), session.CountIncorrect.ToString());
        }

        AnsiConsole.WriteLine("SESSION RESULTS:");
        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
    }


}
