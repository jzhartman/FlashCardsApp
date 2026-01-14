using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;

namespace FlashCards.Application.UseCases.Stacks;

public class GetAllStacksHandler
{
    private readonly IStackRepository _stackRepo;
    private readonly ICardRepository _cardRepo;

    public GetAllStacksHandler(IStackRepository stackRepo, ICardRepository cardRepo)
    {
        _stackRepo = stackRepo;
        _cardRepo = cardRepo;
    }

    public List<StackNameAndCardCountResponse> Handle()
    {
        var stacks = _stackRepo.GetAllNames();

        return MapStack(stacks);
    }

    private List<StackNameAndCardCountResponse> MapStack(List<string> names)
    {
        var stackResponses = new List<StackNameAndCardCountResponse>();

        foreach (var name in names)
        {
            int cardCount = _cardRepo.GetCardCountByStackName(name);
            var stackResponse = new StackNameAndCardCountResponse { Name = name, CardCount = cardCount };
            stackResponses.Add(stackResponse);
        }

        return stackResponses;
    }
}
