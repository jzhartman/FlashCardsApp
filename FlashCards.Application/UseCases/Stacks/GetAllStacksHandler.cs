using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

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
        var stacks = _stackRepo.GetAllStacks();

        return MapStack(stacks);
    }

    private List<StackNameAndCardCountResponse> MapStack(List<Stack> stacks)
    {
        var stackResponses = new List<StackNameAndCardCountResponse>();

        foreach (var stack in stacks)
        {
            int cardCount = _cardRepo.GetCardCountByStackName(stack.Name);
            var stackResponse = new StackNameAndCardCountResponse { Name = stack.Name, CardCount = cardCount };
            stackResponses.Add(stackResponse);
        }

        return stackResponses;
    }
}
