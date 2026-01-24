namespace FlashCards.Core.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public int StackId { get; set; }
        public string FrontText { get; set; }
        public string BackText { get; set; }
        public Stack Stack { get; set; }
        public int TimesIncorrect { get; set; }
        public int TimesCorrect { get; set; }
        public int TimesStudied { get; set; }


        public Card() { }

        public Card(int stackId, string frontText, string backText)
        {
            StackId = stackId;
            FrontText = frontText;
            BackText = backText;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
