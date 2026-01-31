using FlashCards.Application.DTOs;

namespace FlashCards.Application.Interfaces;

public interface IAddStudySessionHandler
{
    void Handle(StudySessionResponse session);
}