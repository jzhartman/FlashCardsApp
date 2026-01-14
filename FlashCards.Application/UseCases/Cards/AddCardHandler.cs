using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public class AddCardHandler
{
    private readonly ICardRepository _repo;

    public AddCardHandler(ICardRepository repo)
    {
        _repo = repo;
    }

    public ValidationResult<Card> Handle(int stackId, string frontText, string backText)
    {
        if (_repo.ExistsByFrontText(frontText, stackId) && _repo.ExistsByBackText(backText, stackId))
            return ValidationResult<Card>.Failure("Card already exists!");

        var errors = new List<string>();
        if (String.IsNullOrWhiteSpace(frontText)) errors.Add("Card front text cannot be blank!");
        if (String.IsNullOrWhiteSpace(backText)) errors.Add("Card back text cannot be blank!");

        if (errors.Count > 0)
            return ValidationResult<Card>.Failure(errors);


        var card = new Card(stackId, frontText, backText);
        var id = _repo.Add(card);
        card.SetId(id);

        return ValidationResult<Card>.Success(card);
    }
}
