using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;

namespace FlashCards.Application.UseCases.Cards;

public class GetAllCardsByStackName
{
    private readonly ICardRepository _repo;

    public GetAllCardsByStackName(ICardRepository repo)
    {
        _repo = repo;
    }

    public List<CardResponse> Handle(string name)
    {
        var cards = _repo.GetAllByStackName(name);

        return BuildResponse(cards);
    }

    private List<CardResponse> BuildResponse(List<Card> cards)
    {
        var outputs = new List<CardResponse>();

        foreach (var card in cards)
        {
            var cardResponse = new CardResponse(card.FrontText, card.BackText);
            outputs.Add(cardResponse);
        }

        return outputs;
    }
}
