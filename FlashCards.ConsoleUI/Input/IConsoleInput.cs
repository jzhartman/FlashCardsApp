namespace FlashCards.ConsoleUI.Input;

public interface IConsoleInput
{
    bool GetDeleteCardConfirmationFromUser(string frontText, string backText);
    bool GetDeleteStackConfirmationFromUser(string stackName, int cardCount);
    int GetRecordIdFromUser(string action, int minValue, int maxValue);
    string GetTextInputFromUser(string message);
    void PressAnyKeyToContinue();
}
