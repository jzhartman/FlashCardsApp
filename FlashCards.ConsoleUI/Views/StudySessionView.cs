using FlashCards.Application.StudySessions.Add;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Views;

public class StudySessionView
{
    public void Render(AddStudySessionCommand session)
    {
        var table = new Table();

        table.AddColumn("Stack Name");
        table.AddColumn("Score");
        table.AddColumn("Time");
        table.AddColumn("# Studied");
        table.AddColumn("# Correct");
        table.AddColumn("# Incorrect");

        table.AddRow(session.StackName, session.Score.ToString("F1"), session.SessionDate.ToString("yyyy-MM-dd HH:mm"), session.CardsStudied.ToString(),
        session.CardsCorrect.ToString(), session.CardsIncorrect.ToString());

        AnsiConsole.WriteLine("SESSION RESULTS:");
        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
    }
}
