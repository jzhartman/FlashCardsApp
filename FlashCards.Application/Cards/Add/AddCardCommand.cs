namespace FlashCards.Application.Cards.Add;

public record AddCardCommand(string StackName, string FrontText, string BackText);
