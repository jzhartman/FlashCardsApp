using FlashCards.Application.UseCases.Stacks;
using FlashCards.Core.Entities;
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
            var stacks = GetAllStacks();
            PrintStackList(stacks);

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
        var handler = _provider.GetRequiredService<AddStackHandler>();
        var id = handler.HandleAdd(input);

        if (id == -1) AnsiConsole.WriteLine("ERROR: Stack already exists with that name");
        else AnsiConsole.WriteLine($"Added stack {input}!");
    }

    private List<Stack> GetAllStacks()
    {
        var handler = _provider.GetRequiredService<GetStacksHandler>();
        return handler.HandleGetAll();
    }

    private string GetNameFromUser()
    {
        AnsiConsole.Markup("Enter stack name: ");
        return Console.ReadLine();
    }

    private void PrintStackList(List<Stack> stacks)
    {
        if (stacks.Count == 0)
            AnsiConsole.MarkupLine("No stacks exist!");
        else
        {
            int i = 1;
            foreach (var stack in stacks)
            {
                AnsiConsole.MarkupLine($"{i}: {stack.Name}");
                i++;
            }
        }
    }
}
