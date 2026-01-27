namespace FlashCards.Core.Entities;

public class Stack
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Card> Cards { get; set; }

    public Stack()
    {

    }

    public Stack(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public Stack(int id, string name, List<Card> cards)
    {
        Id = id;
        Name = name;
        Cards = cards;
    }
}
