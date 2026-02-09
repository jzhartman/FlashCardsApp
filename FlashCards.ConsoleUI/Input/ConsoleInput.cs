using Spectre.Console;

namespace FlashCards.ConsoleUI.Input;

public class ConsoleInput : IConsoleInput
{
    public int GetRecordIdFromUser(string message, int minValue, int maxValue)
    {
        var id = AnsiConsole.Prompt(
            new TextPrompt<int>(message)
            .Validate(input =>
            {
                if (input < minValue) return Spectre.Console.ValidationResult.Error($"[red]ERROR:[/] A record for this value does not exist. Please enter a value between [yellow]{minValue}[/] and [yellow]{maxValue}[/].\r\n");
                else if (input > maxValue) return Spectre.Console.ValidationResult.Error($"[red]ERROR:[/] A record for this value does not exist. Please enter a value between [yellow]{minValue}[/] and [yellow]{maxValue}[/].\r\n");
                else return Spectre.Console.ValidationResult.Success();
            }));

        return id;
    }


    public bool GetDeleteStackConfirmationFromUser(string stackName, int cardCount)
    {
        AddEmptyLines(1);

        string promptText = $"[yellow]WARNING![/]You are about to delete the stack [green]{stackName}[/] and all [blue]{cardCount}[/] cards included in it.";

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>(promptText)
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        return confirmation;
    }

    public bool GetDeleteCardConfirmationFromUser(string frontText, string backText)
    {
        AddEmptyLines(1);

        string promptText = $"[yellow]WARNING![/]You are about to delete the card with the following data:" +
            $"\r\n[green]Front Text:[/] {frontText}" +
            $"\r\n[green]Back Text:[/] {backText}" +
            "\r\n\r\nWould you like to proceed?";

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>(promptText)
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        return confirmation;
    }

    public bool GetEditCardConfirmationFromUser(string originalFrontText, string originalBackText, string newFrontText, string newBackText)
    {
        AddEmptyLines(1);

        string promptText = $"[yellow]WARNING![/]The following change(s) are being made to a card:";

        if (originalFrontText != newFrontText)
            promptText += $"\r\n\t[blue]Front Text[/] changed from: [green]{originalFrontText}[/] to [yellow]{newFrontText}[/]";
        if (originalBackText != newBackText)
            promptText += $"\r\n\t[blue]Back Text[/] changed from: [green]{originalBackText}[/] to [yellow]{newBackText}[/]";

        promptText += "\r\n\r\nConfirm changes: ";


        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>(promptText)
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        return confirmation;
    }



    public bool GetPassStateFromUser()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Did you pass or fail?")
            .AddChoices("PASS", "FAIL"));

        return (choice == "PASS" ? true : false);
    }

    public bool ContinueStudyMode()
    {
        Console.Write("Press \'ESC\' to exit study mode, or any other key to continue...");

        return Console.ReadKey().Key != ConsoleKey.Escape;

    }



    public string GetTextInputFromUser(string message, int topSpaces = 0, int bottomSpaces = 0)
    {
        AddEmptyLines(topSpaces);
        AnsiConsole.Markup($"{message}: ");
        AddEmptyLines(bottomSpaces);
        return Console.ReadLine();
    }

    public void PressAnyKeyToFlipCard()
    {
        Console.Write("Press any key to flip card...");
        Console.ReadKey();
    }

    public void PressAnyKeyToContinue(int topSpaces = 1, string message = "Press any key to continue...")
    {
        AddEmptyLines(topSpaces);
        Console.Write(message);
        Console.ReadKey();
    }

    private void AddEmptyLines(int count)
    {
        for (int i = 0; i < count; i++) AnsiConsole.WriteLine();
    }

}
