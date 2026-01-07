using FlashCards.Application.Interfaces;
using FlashCards.Application.Services;
using FlashCards.Core.Entities;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Cards;

public class AddCardHandler
{
    private readonly ICardRepository _repo;
    private readonly CardUniquenessService _cardUniqueness;

    public AddCardHandler(ICardRepository repo, CardUniquenessService cardUniqueness)
    {
        _repo = repo;
        _cardUniqueness = cardUniqueness;
    }

    public ValidationResult<Card> HandleAdd(int stackId, string frontText, string backText)
    {
        // Build in validation with a ValidationResult object
        if (_cardUniqueness.IsCardUnique(frontText, backText, stackId) == false)
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
