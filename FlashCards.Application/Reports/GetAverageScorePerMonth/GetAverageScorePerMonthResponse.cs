namespace FlashCards.Application.Reports.GetAverageScorePerMonth;

public record GetAverageScorePerMonthResponse(int StackId, string StackName, double[] AverageScoresByMonth, int Year);

