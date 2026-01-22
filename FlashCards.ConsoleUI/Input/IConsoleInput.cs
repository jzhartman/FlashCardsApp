namespace FlashCards.ConsoleUI.Input;

public interface IConsoleInput
{
    bool GetDeleteCardConfirmationFromUser(string frontText, string backText);
    bool GetDeleteStackConfirmationFromUser(string stackName, int cardCount);
    bool GetEditCardConfirmationFromUser(string originalFrontText, string originalBackText, string newFrontText, string newBackText);
    int GetRecordIdFromUser(string action, int minValue, int maxValue);
    string GetTextInputFromUser(string message, int topSpaces = 0, int bottomSpaces = 0);
    void PressAnyKeyToContinue(int topSpaces = 1);
}
