using FlashCards.Application.Reports.GetAverageScorePerMonth;
using FlashCards.Application.Reports.GetSessionCountPerMonth;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Views;

namespace FlashCards.ConsoleUI.Services;

public class ReportService
{
    private readonly ConsoleInput _input;
    private readonly SessionCountPerMonthView _sessionnCountView;
    private readonly AverageScorePerMonthView _averageScoreView;
    private readonly GetAverageScorePerMonthByYearHandler _averageScoreReport;
    private readonly GetSessionCountPerMonthHandler _sessionCountReport;
    private readonly SessionYearSelectionView _yearSelection;

    public ReportService(GetAverageScorePerMonthByYearHandler averageScoreReport, GetSessionCountPerMonthHandler sessionCountReport,
                        SessionCountPerMonthView sessionCountView, AverageScorePerMonthView averageScoreView,
                        SessionYearSelectionView yearSelection, ConsoleInput input)
    {
        _input = input;
        _averageScoreReport = averageScoreReport;
        _sessionCountReport = sessionCountReport;
        _sessionnCountView = sessionCountView;
        _averageScoreView = averageScoreView;
        _yearSelection = yearSelection;
    }

    public void Run(int[] sessionYears)
    {

        var year = _yearSelection.Render(sessionYears);

        var averageScoreReports = _averageScoreReport.Handle(year);
        var sessionCountReports = _sessionCountReport.Handle(year);

        _averageScoreView.Render(averageScoreReports, year);
        _sessionnCountView.Render(sessionCountReports, year);

        _input.PressAnyKeyToContinue(1, "Press any key to return to the Main Menu...");
    }
}
