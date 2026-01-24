using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public class AddCardHandler
{
    private readonly ICardRepository _cardRepo;
    private readonly IStackRepository _stackRepo;

    public AddCardHandler(ICardRepository cardRepo, IStackRepository stackRepo)
    {
        _cardRepo = cardRepo;
        _stackRepo = stackRepo;
    }

    public Result<CardResponse> Handle(string stackName, string frontText, string backText)
    {
        var stackId = _stackRepo.GetIdByName(stackName);

        var card = new Card(stackId, frontText, backText);
        var id = _cardRepo.Add(card);
        card.SetId(id);

        return Result<CardResponse>.Success(new(id, frontText, backText, 0, 0, 0));
    }
}
