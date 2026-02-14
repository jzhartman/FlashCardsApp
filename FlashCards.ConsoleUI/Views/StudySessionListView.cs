using FlashCards.Application.StudySessions.Add;
using FlashCards.Application.StudySessions.GetAll;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Views;

public class StudySessionListView
{
    public void Render(AddStudySessionCommand session)
    {
        var mappedSession = new StudySessionResponse(
            session.SessionDate,
            session.StackName,
            session.Score,
            session.CardsStudied,
            session.CardsCorrect,
            session.CardsIncorrect
        );
        var sessions = new List<StudySessionResponse> { mappedSession };

        Render(sessions);
    }
    public void Render(StudySessionResponse session)
    {
        var sessions = new List<StudySessionResponse> { session };

        Render(sessions);
    }
    public void Render(List<StudySessionResponse> sessions)
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
            table.AddRow(session.StackName, session.Score.ToString("F1"), session.SessionDate.ToString("yyyy-MM-dd HH:mm"), session.CountStudied.ToString(),
            session.CountCorrect.ToString(), session.CountIncorrect.ToString());
        }

        AnsiConsole.WriteLine("SESSION RESULTS:");
        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
    }
}
