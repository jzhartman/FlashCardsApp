namespace FlashCards.Application.DTOs;

public record AddCardCommand(string StackName, string FrontText, string BackText);
