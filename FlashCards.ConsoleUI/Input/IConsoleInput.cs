namespace FlashCards.ConsoleUI.Input;

public interface IConsoleInput
{
    int GetRecordIdFromUser(string action, int minValue, int maxValue);
}
