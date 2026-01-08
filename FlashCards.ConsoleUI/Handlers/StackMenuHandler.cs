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
            Console.Clear();
            AnsiConsole.MarkupLine("[bold green]Manage Stack Menu[/]\r\n");

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
                case "View Stack":
                    var stack = GetStackSelectionFromUser(stacks, "view");
                    HandleViewStack(stack);
                    break;
                case "Add Stack": HandleAddStack(); break;
                case "Delete Stack": HandleDeleteStack(); break;
                case "Return to Main Menu": return;
                default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
            }
        }
    }

    private void HandleViewStack(Stack stack)
    {
        AnsiConsole.MarkupLine($"Viewing stack {stack.Name}!");
        _viewStackMenu.Run(stack);
    }

    private Stack GetStackSelectionFromUser(List<Stack> stacks, string action)
    {
        AnsiConsole.Write($"Enter ID of the stack you wish to {action}: ");
        int id = Int32.Parse(Console.ReadLine());
        return stacks[id - 1];
    }

    private void HandleDeleteStack()
    {
        AnsiConsole.MarkupLine("Handling the delete...");
    }

    private void HandleAddStack()
    {
        var input = GetNameFromUser();
        var handler = _provider.GetRequiredService<AddStackHandler>();
        var result = handler.HandleAdd(input);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                AnsiConsole.WriteLine(error);
            }
        }

        else AnsiConsole.WriteLine($"Added stack {result.Value.Name}!");
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

            Console.WriteLine($"ID  NAME\tCARD COUNT");
            foreach (var stack in stacks)
            {
                var cardCount = (stack.Cards != null) ? stack.Cards.Count : 0;
                AnsiConsole.MarkupLine($"{i}: {stack.Name}\t{cardCount}");
                i++;
            }
        }
        Console.WriteLine();
    }
}
