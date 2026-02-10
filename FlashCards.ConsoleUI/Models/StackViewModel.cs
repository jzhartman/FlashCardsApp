using FlashCards.Application.Cards;

namespace FlashCards.ConsoleUI.Models;

public class StackViewModel
{
    public int StackId { get; set; }
    public string Name { get; set; }
    public List<CardResponse> Cards { get; set; }

    public StackViewModel(int stackId, string name, List<CardResponse> cards)
    {
        StackId = stackId;
        Name = name;
        Cards = cards;
    }
}
