namespace FlashCards.Application.Cards.EditTextBySide;

public record EditCardCommand(int StackId, string FrontText, string BackText);