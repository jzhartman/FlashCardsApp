using FlashCards.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Handlers;

public class StackMenuHandler
{
    private readonly IServiceProvider _provider;
    private readonly ViewStackMenuHandler _viewStackMenu;

    public StackMenuHandler(IServiceProvider provider, ViewStackMenuHandler viewStackMenu)
    {
        _provider = provider;
        _viewStackMenu = viewStackMenu;
    }

    public void Run()
    {
        while (true)
        {
            PrintStackList();

            var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                .Title("Select from the options below:")
                .AddChoices(new[]
                {
                        "View Stack",
                        "Add Stack",
                        "Delete Stack",
                        "Return to Main Menu"
                })
            );

            switch (selection)
            {
                case "View Stack": HandleViewStack(); break;
                case "Add Stack": HandleAddStack(); break;
                case "Delete Stack": HandleDeleteStack(); break;
                case "Return to Main Menu": return;
                default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
            }
        }
    }

    private void HandleViewStack()
    {
        AnsiConsole.MarkupLine("Oh but which stack to view...?");
        _viewStackMenu.Run();
    }

    private void HandleDeleteStack()
    {
        AnsiConsole.MarkupLine("Handling the delete...");
    }

    private void HandleAddStack()
    {
        var input = GetNameFromUser();
        var handler = _provider.GetRequiredService<CreateStackHandler>();
        var id = handler.Handle(input);
    }

    private string GetNameFromUser()
    {
        AnsiConsole.Markup("Enter stack name: ");
        return Console.ReadLine();
    }

    private void PrintStackList()
    {
        AnsiConsole.MarkupLine("This is a printed list of all the stacks....");
    }
}
