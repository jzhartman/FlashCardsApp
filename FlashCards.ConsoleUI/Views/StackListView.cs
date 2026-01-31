using FlashCards.Application.DTOs;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Views;

public class StackListView
{
    public void Render(List<StackNameAndCardCountResponse> stacks)
    {
        if (stacks.Count == 0)
            AnsiConsole.MarkupLine("No stacks exist!");
        else
        {
            int i = 1;
            var table = new Table()
                            .RoundedBorder()
                            .BorderColor(Color.Blue)
                            .ShowRowSeparators();

            table.AddColumn("Id");
            table.AddColumn("Stack Name");
            table.AddColumn("Card Count");

            foreach (var stack in stacks)
            {
                table.AddRow(i.ToString(), stack.Name, stack.CardCount.ToString());
                i++;
            }

            AnsiConsole.Write(table);
        }
        Console.WriteLine();
    }
}
