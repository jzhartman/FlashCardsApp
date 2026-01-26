namespace FlashCards.ConsoleUI.Enums;

public enum MainMenuItem
{
    Review,
    Create,
    Delete,
    Study,
    Past,
    Report,
    Exit
}

public static class MainMenuItemExtensions
{
    public static string ToFriendlyString(this MainMenuItem menu)
    {
        switch (menu)
        {
            case MainMenuItem.Review:
                return "Review Cards in Stack";
            case MainMenuItem.Create:
                return "Create New Stack";
            case MainMenuItem.Delete:
                return "Delete Stack";
            case MainMenuItem.Study:
                return "Begin Study Session";
            case MainMenuItem.Past:
                return "View Past Study Sessions";
            case MainMenuItem.Report:
                return "View Reports";
            case MainMenuItem.Exit:
                return "Exit";
            default:
                return "Get your damn dirty hands off me you FILTHY APE!";
        }
    }
}

