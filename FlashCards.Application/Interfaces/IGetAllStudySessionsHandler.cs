using FlashCards.Application.DTOs;

namespace FlashCards.Application.Interfaces;

public interface IGetAllStudySessionsHandler
{
    List<StudySessionResponse> Handle();
}