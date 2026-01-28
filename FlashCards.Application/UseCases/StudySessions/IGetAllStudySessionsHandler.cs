using FlashCards.Application.DTOs;

namespace FlashCards.Application.UseCases.StudySessions;

public interface IGetAllStudySessionsHandler
{
    List<StudySessionResponse> Handle();
}