using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.Reports.GetAverageScorePerMonth;

public class GetAverageScorePerMonthByYearHandler
{
    private readonly IStudySessionRepository _studyRepo;

    public GetAverageScorePerMonthByYearHandler(IStudySessionRepository studyRepo)
    {
        _studyRepo = studyRepo;
    }

    public List<GetAverageScorePerMonthResponse> Handle(int year)
    {
        var report = _studyRepo.GetAverageScoreByMonthByYear(year);

        return ReportMapper(report);
    }

    private List<GetAverageScorePerMonthResponse> ReportMapper(List<SessionReport> report)
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
