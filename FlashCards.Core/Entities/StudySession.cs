using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Core.Entities;

public class StudySession
{
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public int StackId { get; set; }
    public int Score { get; set; }
    public int TotalCards { get; set; }

    public double GetPercentageScore()
    {
        if (TotalCards == 0) return 0;
        return (double)Score / TotalCards * 100;
    }

}
