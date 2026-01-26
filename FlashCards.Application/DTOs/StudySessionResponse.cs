namespace FlashCards.Application.DTOs;

public record StudySessionResponse(DateTime Time, string StackName, double Score, int CountStudied, int CountCorrect, int CountIncorrect);

