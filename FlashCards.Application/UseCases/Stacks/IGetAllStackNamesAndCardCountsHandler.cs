using FlashCards.Application.DTOs;

namespace FlashCards.Application.UseCases.Stacks;

public interface IGetAllStackNamesAndCardCountsHandler
{
    List<StackNameAndCardCountResponse> Handle();
}