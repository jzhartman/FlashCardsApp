using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using FlashCards.Core.Validation;

namespace FlashCards.Application.Cards.GetAllByStackId;

public class GetAllByStackId

{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;

    public GetAllByStackId(ICardRepository cardRepo, IStackRepository stackRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
    }

    public Result<List<CardResponse>> Handle(int stackId)
    {
        if (_stackRepo.ExistsById(stackId) == false)
            return Result<List<CardResponse>>.Failure(Errors.NoStacksExist);

        var cards = _cardRepo.GetAllByStackId(stackId);

        if (cards.Count == 0)
            return Result<List<CardResponse>>.Failure(Errors.NoCardsExist);
        else
            return Result<List<CardResponse>>.Success(BuildResponse(cards, stackId));
    }

    private List<CardResponse> BuildResponse(List<Card> cards, int stackId)
    {
        var outputs = new List<CardResponse>();

        foreach (var card in cards)
        {
            var cardResponse = new CardResponse(card.Id, stackId, card.FrontText, card.BackText, card.TimesStudied, card.TimesCorrect, card.TimesIncorrect);
            outputs.Add(cardResponse);
        }

        return outputs;
    }
}
