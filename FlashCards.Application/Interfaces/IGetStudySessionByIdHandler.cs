using FlashCards.Application.DTOs;

namespace FlashCards.Application.Interfaces;

public interface IGetStudySessionByIdHandler
{
    List<StudySessionResponse> Handle(StackNameAndCardCountResponse stack);
}