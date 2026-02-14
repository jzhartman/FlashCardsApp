using FlashCards.Application.Reports.GetAverageScorePerMonth;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Views;

public class AverageScorePerMonthView
{
    public void Render(GetAverageScorePerMonthResponse averageScores)
    {
        int i = 1;
        var table = new Table()
                        .RoundedBorder()
                        .BorderColor(Color.Blue)
                        .ShowRowSeparators();

        table.AddColumn("Stack Name");
        table.AddColumn("January");
        table.AddColumn("February");
        table.AddColumn("March");
        table.AddColumn("April");
        table.AddColumn("May");
        table.AddColumn("June");
        table.AddColumn("July");
        table.AddColumn("August");
        table.AddColumn("September");
        table.AddColumn("October");
        table.AddColumn("November");
        table.AddColumn("December");
        table.Title = new TableTitle($"Year: {averageScores.Year}");


        table.AddRow(averageScores.StackName,
            averageScores.January.ToString(),
            averageScores.February.ToString(),
            averageScores.March.ToString(),
            averageScores.April.ToString(),
            averageScores.May.ToString(),
            averageScores.June.ToString(),
            averageScores.July.ToString(),
            averageScores.August.ToString(),
            averageScores.September.ToString(),
            averageScores.October.ToString(),
            averageScores.November.ToString(),
            averageScores.December.ToString());

        AnsiConsole.Write(table);

        Console.WriteLine();
    }
}
