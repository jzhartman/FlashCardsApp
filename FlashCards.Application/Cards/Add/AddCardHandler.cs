using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using FlashCards.Core.Validation;

namespace FlashCards.Application.Cards.Add;

public class AddCardHandler
{
    private readonly ICardRepository _cardRepo;

    public AddCardHandler(ICardRepository cardRepo)
    {
        _cardRepo = cardRepo;
    }

    public Result<CardResponse> Handle(AddCardCommand cardCommand)
    {
        var card = new Card(cardCommand.StackId, cardCommand.FrontText, cardCommand.BackText);
        var id = _cardRepo.Add(card);
        card.SetId(id);

        if (id > 0) return Result<CardResponse>.Success(new(id, cardCommand.StackId, cardCommand.FrontText, cardCommand.BackText, 0, 0, 0));
        else return Result<CardResponse>.Failure(Errors.InvalidId);
    }
}
