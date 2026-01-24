namespace FlashCards.Core.Entities;

public class StudySession
{
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public int StackId { get; set; }
    public int Score { get; set; }
    public int CountStudied { get; set; }
    public int CountCorrect { get; set; }
    public int CountIncorrect { get; set; }

    public double GetPercentageScore()
    {
        if (CountStudied == 0) return 0;
        return (double)Score / CountStudied * 100;
    }

}
