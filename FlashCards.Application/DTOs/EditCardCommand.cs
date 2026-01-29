namespace FlashCards.Application.DTOs;

public record EditCardCommand(string StackName, string FrontText, string BackText);