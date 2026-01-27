using FlashCards.Core.Entities;

namespace FlashCards.Application.Interfaces;

public interface IStudySessionRepository
{
    void Add(StudySession session);
    List<StudySession> GetAll();
    StudySession GetByStackId(int id);
}
