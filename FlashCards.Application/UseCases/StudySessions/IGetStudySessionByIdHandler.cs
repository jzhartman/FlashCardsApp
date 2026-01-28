using FlashCards.Application.DTOs;

namespace FlashCards.Application.UseCases.StudySessions;

public interface IGetStudySessionByIdHandler
{
    StudySessionResponse Handle(StackNameAndCardCountResponse stack);
}