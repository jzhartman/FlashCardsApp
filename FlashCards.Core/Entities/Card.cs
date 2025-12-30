namespace FlashCards.Core.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public int StackId { get; set; }
        public string FrontText { get; set; }
        public string BackText { get; set; }

        public Card(int stackId, string frontText, string backText)
        {
            StackId = stackId;
            FrontText = frontText;
            BackText = backText;
        }

        public Card(int id, int stackId, string frontText, string backText)
        {
            Id = id;
            StackId = stackId;
            FrontText = frontText;
            BackText = backText;
        }
    }
}
