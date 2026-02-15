using FlashCards.Core.Entities;

namespace FlashCards.Application.Interfaces;

public interface IStudySessionRepository
{
    void Add(StudySession session);
    List<StudySession> GetAll();
    int GetSessionCountByStackId(int id);
    void DeleteAllByStackId(int id);
    List<SessionReport> GetAverageScoreByMonthByYear(int year);
    int[] GetAllSessionYears();
    List<SessionReport> GetSessionCountByMonthByYear(int year);
}
