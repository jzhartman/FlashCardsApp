using FlashCards.Application.Cards;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Views;

public class CardListView
{
    public void Render(CardResponse card)
    {
        Render(new List<CardResponse>() { card });
    }
    public void Render(List<CardResponse> cards)
    {
        int i = 1;
        var table = new Table()
                        .RoundedBorder()
                        .BorderColor(Color.Blue)
                        .ShowRowSeparators();

        table.AddColumn("Id");
        table.AddColumn("Front Text");
        table.AddColumn("Back Text");

        if (cards.Count == 0) table.AddRow("<NO ID>", "<NO TEXT>", "<NO TEXT>");

        foreach (var card in cards)
        {
            table.AddRow(i.ToString(), card.FrontText, card.BackText);
            i++;
        }

        AnsiConsole.Write(table);

        Console.WriteLine();
    }
}
