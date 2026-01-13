using FlashCards.Application.Interfaces;
using FlashCards.Core.Entities;
using FlashCards.Core.Validation;

namespace FlashCards.Application.UseCases.Decks;

public class AddDeckHandler
{
    private readonly IDeckRepository _repo;

    public AddDeckHandler(IDeckRepository repo)
    {
        _repo = repo;
    }

    public ValidationResult<Deck> Handle(string name)
    {
        if (_repo.ExistsByName(name))
            return ValidationResult<Deck>.Failure("Deck name must be unique!");

        if (String.IsNullOrWhiteSpace(name))
            return ValidationResult<Deck>.Failure("Deck name cannot be empty!");

        var id = _repo.Add(name);
        var deck = new Deck(id, name, new List<Card>());

        return ValidationResult<Deck>.Success(deck);
    }

}