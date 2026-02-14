namespace FlashCards.Application.StudySessions.GetAll;

public record StudySessionResponse(DateTime SessionDate, string StackName, double Score, int CountStudied, int CountCorrect, int CountIncorrect);

