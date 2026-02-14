namespace FlashCards.Core.Entities;

public class StudySession
{
    public int Id { get; set; }
    public DateTime SessionDate { get; set; }
    public int StackId { get; set; }
    public double Score { get; set; }
    public int CountStudied { get; set; }
    public int CountCorrect { get; set; }
    public int CountIncorrect { get; set; }
}
