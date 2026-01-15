using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;

namespace FlashCards.Application.UseCases.Stacks;

public class GetAllStackNamesAndCardCountsHandler
{
    private readonly IStackRepository _stackRepo;
    private readonly ICardRepository _cardRepo;

    public GetAllStackNamesAndCardCountsHandler(IStackRepository stackRepo, ICardRepository cardRepo)
    {
        _stackRepo = stackRepo;
        _cardRepo = cardRepo;
    }

    public List<StackNameAndCardCountResponse> Handle()
    {
        var stacks = _stackRepo.GetAllNames();

        return BuildResponse(stacks);
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
