using FlashCards.Application.Reports.GetSessionCountPerMonth;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Views;

public class SessionCountPerMonthView
{
    public void Render(List<GetSessionCountPerMonthResponse> reports, int year)
    {
        int i = 1;
        var table = new Table()
                        .RoundedBorder()
                        .BorderColor(Color.Blue)
                        .ShowRowSeparators();

        table.AddColumn("Stack Name", col => col.NoWrap().LeftAligned());
        table.AddColumn("Jan", col => col.Width(5).Centered());
        table.AddColumn("Feb", col => col.Width(5).Centered());
        table.AddColumn("Mar", col => col.Width(5).Centered());
        table.AddColumn("Apr", col => col.Width(5).Centered());
        table.AddColumn("May", col => col.Width(5).Centered());
        table.AddColumn("Jun", col => col.Width(5).Centered());
        table.AddColumn("Jul", col => col.Width(5).Centered());
        table.AddColumn("Aug", col => col.Width(5).Centered());
        table.AddColumn("Sep", col => col.Width(5).Centered());
        table.AddColumn("Oct", col => col.Width(5).Centered());
        table.AddColumn("Nov", col => col.Width(5).Centered());
        table.AddColumn("Dec", col => col.Width(5).Centered());
        table.AddColumn("Annual");
        table.Title = new TableTitle($"Count of Sessions Per Month for Year: {year}");


        foreach (var report in reports)
        {
            table.AddRow(report.StackName,
            report.January.ToString(),
            report.February.ToString(),
            report.March.ToString(),
            report.April.ToString(),
            report.May.ToString(),
            report.June.ToString(),
            report.July.ToString(),
            report.August.ToString(),
            report.September.ToString(),
            report.October.ToString(),
            report.November.ToString(),
            report.December.ToString(),
            AnnualSum(report));
        }

        AnsiConsole.Write(table);

        Console.WriteLine();
    }

    private string AnnualSum(GetSessionCountPerMonthResponse report)
    {
        return ((double)(report.January + report.February + report.March + report.April + report.May + report.June + report.July
                + report.August + report.September + report.October + report.November + report.December)).ToString();
    }
}
