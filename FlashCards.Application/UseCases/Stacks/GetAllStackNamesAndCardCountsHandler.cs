using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Stacks;

public class GetAllStackNamesAndCardCountsHandler : IGetAllStackNamesAndCardCountsHandler
{
    private readonly IStackRepository _stackRepo;
    private readonly ICardRepository _cardRepo;

    public GetAllStackNamesAndCardCountsHandler(IStackRepository stackRepo, ICardRepository cardRepo)
    {
        _stackRepo = stackRepo;
        _cardRepo = cardRepo;
    }

    public Result<List<StackNameAndCardCountResponse>> Handle()
    {
        var stacks = _stackRepo.GetAllNames();

        if (stacks.Count == 0)
            return Result<List<StackNameAndCardCountResponse>>.Failure(Errors.NoStacksExist);
        else
            return Result<List<StackNameAndCardCountResponse>>.Success(BuildResponse(stacks));
    }

    private List<StackNameAndCardCountResponse> BuildResponse(List<string> names)
    {
        var stackResponses = new List<StackNameAndCardCountResponse>();

        foreach (var name in names)
        {
            int cardCount = _cardRepo.GetCardCountByStackName(name);
            var stackResponse = new StackNameAndCardCountResponse(name, cardCount);
            stackResponses.Add(stackResponse);
        }

        return stackResponses;
    }
}
