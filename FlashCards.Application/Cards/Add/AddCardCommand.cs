namespace FlashCards.Application.Cards.Add;

public record AddCardCommand(int StackId, string FrontText, string BackText);
