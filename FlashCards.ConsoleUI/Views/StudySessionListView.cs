using FlashCards.Application.StudySessions.GetAll;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Views;

public class StudySessionListView
{
    public void Render(List<StudySessionResponse> sessions, int startIndex)
    {
        var table = new Table();

        table.AddColumn("Id");
        table.AddColumn("Stack Name");
        table.AddColumn("Score");
        table.AddColumn("Time");
        table.AddColumn("# Studied");
        table.AddColumn("# Correct");
        table.AddColumn("# Incorrect");

        for (int i = startIndex; i < startIndex + 15; i++)
        {
            if (i < sessions.Count - 1)
            {
                table.AddRow((i + 1).ToString(),
                                sessions[i].StackName,
                                sessions[i].Score.ToString("F1"),
                                sessions[i].SessionDate.ToString("yyyy-MM-dd HH:mm"),
                                sessions[i].CountStudied.ToString(),
                                sessions[i].CountCorrect.ToString(),
                                sessions[i].CountIncorrect.ToString());
            }
            else
            {
                table.AddRow("", "", "", "", "", "", "");
            }
        }

        AnsiConsole.WriteLine("SESSION RESULTS:");
        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
    }
}
