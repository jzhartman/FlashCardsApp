namespace FlashCards.Application.DTOs;

public record CardResponse(int Id, string FrontText, string BackText, int TimesStudied, int TimesCorrect, int TimesIncorrect);
