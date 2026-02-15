namespace FlashCards.Application.Reports.GetSessionCountPerMonth;

public record GetSessionCountPerMonthResponse(int StackId, string StackName, string Year, double January, double February,
    double March, double April, double May, double June, double July, double August, double September, double October,
    double November, double December);
