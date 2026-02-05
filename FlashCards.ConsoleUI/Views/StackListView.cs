using FlashCards.Application.Stacks.GetAll;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Views;

public class StackListView
{
    public void Render(StackNameAndCardCountResponse stack)
    {
        Render(new List<StackNameAndCardCountResponse>() { stack });
    }
    public void Render(List<StackNameAndCardCountResponse> stacks)
    {
        int i = 1;
        var table = new Table()
                        .RoundedBorder()
                        .BorderColor(Color.Blue)
                        .ShowRowSeparators();

        table.AddColumn("Id");
        table.AddColumn("Stack Name");
        table.AddColumn("Card Count");

        if (stacks.Count == 0) table.AddRow("<NO ID>", "<NO NAME>", "<NO CARDS>");

        foreach (var stack in stacks)
        {
            table.AddRow(i.ToString(), stack.Name, stack.CardCount.ToString());
            i++;
        }

        AnsiConsole.Write(table);

        Console.WriteLine();
    }
}
