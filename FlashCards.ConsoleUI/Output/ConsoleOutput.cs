using FlashCards.Application.DTOs;
using FlashCards.Core.Validation;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Output;

public class ConsoleOutput : IConsoleOutput
{
    public void PrintPageTitle(string title)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine($"[bold green]{title.ToUpper()}[/]\r\n");
    }

    public void PrintStackList(List<StackNameAndCardCountResponse> stacks)
    {
        if (stacks.Count == 0)
            AnsiConsole.MarkupLine("No stacks exist!");
        else
        {
            int i = 1;

            Console.WriteLine($"ID  NAME\tCARD COUNT");
            foreach (var stack in stacks)
            {
                AnsiConsole.MarkupLine($"{i}: {stack.Name}\t{stack.CardCount}");
                i++;
            }
        }
        Console.WriteLine();
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

    public void PrintSessionResults(string stackName, int count, int cardsCorrect, int cardsIncorrect)
    {
        var cardsStudied = cardsCorrect + cardsIncorrect;
        var cardsNotStudied = count - cardsStudied;

        var table = new Table();

        table.AddColumn("Stack Name");
        table.AddColumn("Date");
        table.AddColumn("# Cards Studied");
        table.AddColumn("# Correct");
        table.AddColumn("# Incorrect");
        table.AddColumn("# Not Studied");


        table.AddRow(stackName, DateTime.Now.ToString("yyyy-MM-dd HH:mm"), cardsStudied.ToString(), cardsCorrect.ToString(),
                    cardsIncorrect.ToString(), cardsNotStudied.ToString());


        AnsiConsole.WriteLine("SESSION RESULTS:");
        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
    }
}
