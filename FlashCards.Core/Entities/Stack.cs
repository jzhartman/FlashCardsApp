namespace FlashCards.Core.Entities;

public class Stack
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Card> Cards { get; set; }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public void SetId(int id)
    {
        Id = id;
    }
}
