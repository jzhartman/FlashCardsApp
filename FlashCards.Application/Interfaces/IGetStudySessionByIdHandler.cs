using FlashCards.Application.DTOs;

namespace FlashCards.Application.Interfaces;

public interface IGetStudySessionByIdHandler
{
    StudySessionResponse Handle(StackNameAndCardCountResponse stack);
}