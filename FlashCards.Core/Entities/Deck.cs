namespace FlashCards.Core.Entities;

public class Deck
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Card> Cards { get; set; }

    public Deck(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public Deck(int id, string name, List<Card> cards)
    {
        Id = id;
        Name = name;
        Cards = cards;
    }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public void SetId(int id)
    {
        Id = id;
    }
}
