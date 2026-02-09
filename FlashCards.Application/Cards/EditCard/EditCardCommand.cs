namespace FlashCards.Application.Cards.EditTextBySide;

public record EditCardCommand(string StackName, string FrontText, string BackText);