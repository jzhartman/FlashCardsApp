namespace FlashCards.ConsoleUI.Input;

public interface IConsoleInput
{
    bool ContinueStudyMode();
    bool GetDeleteCardConfirmationFromUser(string frontText, string backText);
    bool GetDeleteStackConfirmationFromUser(string stackName, int cardCount);
    bool GetEditCardConfirmationFromUser(string originalFrontText, string originalBackText, string newFrontText, string newBackText);
    bool GetPassStateFromUser();
    int GetRecordIdFromUser(string message, int minValue, int maxValue);
    string GetTextInputFromUser(string message, int topSpaces = 0, int bottomSpaces = 0);
    void PressAnyKeyToContinue(int topSpaces = 1, string message = "Press any key to continue...");
}
