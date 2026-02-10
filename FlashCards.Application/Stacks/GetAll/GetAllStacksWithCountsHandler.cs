using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using FlashCards.Core.Validation;

namespace FlashCards.Application.Stacks.GetAll;

public class GetAllStacksWithCountsHandler
{
    private readonly IStackRepository _stackRepo;
    private readonly ICardRepository _cardRepo;
    private readonly IStudySessionRepository _sessionRepo;

    public GetAllStacksWithCountsHandler(IStackRepository stackRepo, ICardRepository cardRepo, IStudySessionRepository sessionRepo)
    {
        _stackRepo = stackRepo;
        _cardRepo = cardRepo;
        _sessionRepo = sessionRepo;
    }

    public Result<List<StackResponseWithCounts>> Handle()
    {
        var stacks = _stackRepo.GetAll();

        if (stacks.Count == 0)
            return Result<List<StackResponseWithCounts>>.Failure(Errors.NoStacksExist);
        else
            return Result<List<StackResponseWithCounts>>.Success(BuildResponse(stacks));
    }

    private List<StackResponseWithCounts> BuildResponse(List<Stack> stacks)
    {
        var stackResponses = new List<StackResponseWithCounts>();

        foreach (var stack in stacks)
        {
            int cardCount = _cardRepo.GetCardCountByStackId(stack.Id);
            var sessionCount = _sessionRepo.GetSessionCountByStackId(stack.Id);

            var stackResponse = new StackResponseWithCounts(stack.Id, stack.Name, cardCount, sessionCount);
            stackResponses.Add(stackResponse);
        }

        return stackResponses;
    }
}
