using FlashCards.Application.DTOs;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Output;

public class ConsoleOutput : IConsoleOutput
{
    public void PrintPageTitle(string title)
    {
        Console.Clear();
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
}
