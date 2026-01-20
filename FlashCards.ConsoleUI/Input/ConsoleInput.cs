using Spectre.Console;

namespace FlashCards.ConsoleUI.Input;

public class ConsoleInput : IConsoleInput
{
    public int GetRecordIdFromUser(string action, int minValue, int maxValue)
    {
        var id = AnsiConsole.Prompt(
            new TextPrompt<int>($"Please enter the [yellow]ID[/] of the record you wish to {action.ToLower()}:")
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

        string promptText = $"[yellow]WARNING![/]You are about to delete the stack [green]{stackName}[/] and all [blue]{cardCount}[/] included in it.";

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
            $"\r\n[green]Back Text:[/] {backText}";

        var confirmation = AnsiConsole.Prompt(
            new TextPrompt<bool>(promptText)
            .AddChoice(true)
            .AddChoice(false)
            .WithConverter(choice => choice ? "y" : "n"));

        return confirmation;
    }







    public string GetTextInputFromUser(string message)
    {
        AnsiConsole.Markup($"{message}: ");
        return Console.ReadLine();
    }

    public void PressAnyKeyToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void AddEmptyLines(int count)
    {
        for (int i = 0; i < count; i++) AnsiConsole.WriteLine();
    }

}
