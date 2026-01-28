using FlashCards.Application.DTOs;

namespace FlashCards.Application.UseCases.StudySessions;

public interface IAddStudySessionHandler
{
    void Handle(StudySessionResponse session);
}