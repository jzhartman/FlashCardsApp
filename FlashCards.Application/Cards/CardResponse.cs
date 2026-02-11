namespace FlashCards.Application.Cards;

public record CardResponse(int Id, int StackId, string FrontText, string BackText, int TimesStudied, int TimesCorrect, int TimesIncorrect);
