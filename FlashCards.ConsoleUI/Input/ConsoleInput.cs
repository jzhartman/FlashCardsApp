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

}
