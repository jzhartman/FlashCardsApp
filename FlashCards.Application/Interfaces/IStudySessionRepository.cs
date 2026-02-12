using FlashCards.Core.Entities;

namespace FlashCards.Application.Interfaces;

public interface IStudySessionRepository
{
    void Add(StudySession session);
    List<StudySession> GetAll();
    void DeleteAllByStackName(string name);
    int GetSessionCountByStackId(int id);
    void DeleteAllByStackId(int id);
}
