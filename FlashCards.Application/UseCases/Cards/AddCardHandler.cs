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

    public Result<Card> Handle(string stackName, string frontText, string backText)
    {
        var stackId = _stackRepo.GetIdByName(stackName);

        if (_cardRepo.ExistsByFrontText(frontText, stackId))
            return Result<Card>.Failure("A card with that front text already exists!");
        if (_cardRepo.ExistsByBackText(backText, stackId))
            return Result<Card>.Failure("A card with that back text already exists!");

        var errors = new List<string>();
        if (String.IsNullOrWhiteSpace(frontText)) errors.Add("Card front text cannot be blank!");
        if (String.IsNullOrWhiteSpace(backText)) errors.Add("Card back text cannot be blank!");

        if (errors.Count > 0)
            return Result<Card>.Failure(errors);


        var card = new Card(stackId, frontText, backText);
        var id = _cardRepo.Add(card);
        card.SetId(id);

        return Result<Card>.Success(card);
    }
}
