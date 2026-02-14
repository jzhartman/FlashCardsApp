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
            averageScores.AverageScoresByMonth[0].ToString(),
            averageScores.AverageScoresByMonth[1].ToString(),
            averageScores.AverageScoresByMonth[2].ToString(),
            averageScores.AverageScoresByMonth[3].ToString(),
            averageScores.AverageScoresByMonth[4].ToString(),
            averageScores.AverageScoresByMonth[5].ToString(),
            averageScores.AverageScoresByMonth[6].ToString(),
            averageScores.AverageScoresByMonth[7].ToString(),
            averageScores.AverageScoresByMonth[8].ToString(),
            averageScores.AverageScoresByMonth[9].ToString(),
            averageScores.AverageScoresByMonth[10].ToString(),
            averageScores.AverageScoresByMonth[11].ToString());

        AnsiConsole.Write(table);

        Console.WriteLine();
    }
}
