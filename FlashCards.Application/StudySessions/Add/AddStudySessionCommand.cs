namespace FlashCards.Application.StudySessions.Add;

public record AddStudySessionCommand(int StackId, string StackName, DateTime SessionDate, double Score,
                                    int CardsStudied, int CardsCorrect, int CardsIncorrect);
