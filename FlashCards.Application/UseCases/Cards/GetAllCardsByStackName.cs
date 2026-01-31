using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public class GetAllCardsByStackName : IGetAllCardsByStackName
{
    private readonly ICardRepository _repo;

    public GetAllCardsByStackName(ICardRepository repo)
    {
        _repo = repo;
    }

    public Result<List<CardResponse>> Handle(string name)
    {
        var cards = _repo.GetAllByStackName(name);

        if (cards.Count == 0)
            return Result<List<CardResponse>>.Failure(Errors.NoCardsExist);
        else
            return Result<List<CardResponse>>.Success(BuildResponse(cards));
    }

    private List<CardResponse> BuildResponse(List<Card> cards)
    {
        var outputs = new List<CardResponse>();

        foreach (var card in cards)
        {
            var cardResponse = new CardResponse(card.Id, card.FrontText, card.BackText, card.TimesStudied, card.TimesCorrect, card.TimesIncorrect);
            outputs.Add(cardResponse);
        }

        return outputs;
    }
}
