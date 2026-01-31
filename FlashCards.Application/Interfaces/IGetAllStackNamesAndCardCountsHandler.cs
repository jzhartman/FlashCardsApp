using FlashCards.Application.DTOs;
using FlashCards.Core.Validation;

namespace FlashCards.Application.Interfaces;

public interface IGetAllStackNamesAndCardCountsHandler
{
    Result<List<StackNameAndCardCountResponse>> Handle();
}