using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.Reports.GetSessionCountPerMonth;

public class GetSessionCountPerMonthHandler
{
    private readonly IStudySessionRepository _studyRepo;

    public GetSessionCountPerMonthHandler(IStudySessionRepository studyRepo)
    {
        _studyRepo = studyRepo;
    }

    public List<GetSessionCountPerMonthResponse> Handle(int year)
    {
        var report = _studyRepo.GetSessionCountByMonthByYear(year);

        return ReportMapper(report);
    }

    private List<GetSessionCountPerMonthResponse> ReportMapper(List<SessionReport> report)
    {
        var mappedReport = new List<GetSessionCountPerMonthResponse>();

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
