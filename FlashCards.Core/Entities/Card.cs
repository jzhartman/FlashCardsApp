namespace FlashCards.Core.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public int DeckId { get; set; }
        public string FrontText { get; set; }
        public string BackText { get; set; }
        public Deck Deck { get; set; }

        public Card(int deckId, string frontText, string backText)
        {
            DeckId = deckId;
            FrontText = frontText;
            BackText = backText;
        }

        public Card(int id, int deckId, string frontText, string backText)
        {
            Id = id;
            DeckId = deckId;
            FrontText = frontText;
            BackText = backText;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
