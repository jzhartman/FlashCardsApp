using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.Reports.GetAverageScorePerMonth;

public class GetAverageScorePerMonthHandler
{
    private readonly IStudySessionRepository _studyRepo;

    public GetAverageScorePerMonthHandler(IStudySessionRepository studyRepo)
    {
        _studyRepo = studyRepo;
    }

    public List<GetAverageScorePerMonthResponse> Handle()
    {
        var report = _studyRepo.GetAverageScoreByMonth();

        return ReportMapper(report);
    }

    private List<GetAverageScorePerMonthResponse> ReportMapper(List<AverageScoreReport> report)
    {
        var mappedReport = new List<GetAverageScorePerMonthResponse>();

        foreach (var row in report)
        {
            mappedReport.Add(new(
                row.StackId,
                row.StackName,
                row.SessionYear,
                row.January,
                row.February,
                row.March,
                row.April,
                row.May,
                row.June,
                row.July,
                row.August,
                row.September,
                row.October,
                row.November,
                row.December
                ));
        }

        return mappedReport;
    }
}
