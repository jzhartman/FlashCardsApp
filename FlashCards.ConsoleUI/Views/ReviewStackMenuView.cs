using FlashCards.ConsoleUI.Enums;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Views;

public class ReviewStackMenuView
{
    public ReviewStackMenuItem Render(ReviewStackMenuItem[] menuItems)
    {
        return AnsiConsole.Prompt(
                    new SelectionPrompt<ReviewStackMenuItem>()
                    .Title("Select from the options below:")
                    .UseConverter(menu => menu switch
                    {
                        ReviewStackMenuItem.ReviewCards => "Review Cards in Stack",
                        ReviewStackMenuItem.AddCard => "Add Card to Stack",
                        ReviewStackMenuItem.EditCard => "Edit Card Text",
                        ReviewStackMenuItem.DeleteCard => "Delete Card from Stack",
                        ReviewStackMenuItem.Return => "Return to Main Menu",
                        _ => menu.ToString()
                    })
                    .AddChoices(menuItems));
    }
}
