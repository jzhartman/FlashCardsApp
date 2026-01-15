using FlashCards.Application.DTOs;
using FlashCards.Application.UseCases.Stacks;
using FlashCards.ConsoleUI.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Controllers;

public class MainMenuHandler
{
    private readonly IServiceProvider _provider;
    private readonly ReviewStackMenuHandler _stackMenu;

    public MainMenuHandler(IServiceProvider provider, ReviewStackMenuHandler stackMenu)
    {
        _provider = provider;
        _stackMenu = stackMenu;
    }
    public void Run()
    {
        bool exitApp = false;

        while (exitApp == false)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold green]Main Menu[/]\r\n");
            var stacks = GetAllStackNamesAndCardCounts();
            PrintStackList(stacks);


            var selection = PrintMainMenuAndGetSelection();
            exitApp = HandleUserSelection(selection, stacks);
        }
    }

    private string PrintMainMenuAndGetSelection()
    {
        return AnsiConsole.Prompt(new SelectionPrompt<string>()
                            .Title("Select from the options below:")
                            .AddChoices(new[]
                            {
                                    "Review Cards in Stack",
                                    "Create New Stack",
                                    "Delete Stack",
                                    "Begin Study Session",
                                    "View Past Study Sessions",
                                    "View Reports",
                                    "Exit"
                            }));
    }

    private bool HandleUserSelection(string selection, List<StackNameAndCardCountResponse> stacks)
    {
        switch (selection)
        {
            case "Review Cards in Stack":
                var stack = GetStackSelectionFromUser(stacks, "review");
                _stackMenu.Run(stack.Name);
                break;
            case "Create New Stack":
                HandleAddStack();
                break;
            case "Delete Stack":
                HandleDeleteStack();
                break;
            case "Begin Study Session":
                HandleStudy();
                break;
            case "View Past Study Sessions":
                HandleViewPastSessions();
                break;
            case "View Reports":
                HandleReports();
                break;
            case "Exit":
                return true;
            default:
                AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!");
                break;
        }

        return false;
    }



    private void PrintStackList(List<StackNameAndCardCountResponse> stacks)
    {
        if (stacks.Count == 0)
            AnsiConsole.MarkupLine("No stacks exist!");
        else
        {
            int i = 1;

            Console.WriteLine($"ID  NAME\tCARD COUNT");
            foreach (var stack in stacks)
            {
                AnsiConsole.MarkupLine($"{i}: {stack.Name}\t{stack.CardCount}");
                i++;
            }
        }
        Console.WriteLine();
    }

    private List<StackNameAndCardCountResponse> GetAllStackNamesAndCardCounts()
    {
        var handler = _provider.GetRequiredService<GetAllStackNamesAndCardCountsHandler>();
        return handler.Handle();
    }

    private StackNameAndCardCountResponse GetStackSelectionFromUser(List<StackNameAndCardCountResponse> stacks, string action)
    {
        AnsiConsole.Write($"Enter ID of the stack you wish to {action}: ");
        int id = Int32.Parse(Console.ReadLine());
        return stacks[id - 1];
    }

    private void HandleAddStack()
    {
        var input = GetNameFromUser();
        var handler = _provider.GetRequiredService<AddStackHandler>();
        var result = handler.Handle(input);

        if (!result.IsValid)
        {
            foreach (var error in result.Errors)
            {
                AnsiConsole.WriteLine(error);
            }
        }

        else AnsiConsole.WriteLine($"Added stack {result.Value.Name}!");
        PressAnyKeyToContinue();
    }

    private string GetNameFromUser()
    {
        AnsiConsole.Markup("Enter stack name: ");
        return Console.ReadLine();
    }
    private void PressAnyKeyToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    //
    // NOT YET IMPLEMENTED
    //

    private void HandleDeleteStack()
    {
        AnsiConsole.MarkupLine("Handling the delete...");
        PressAnyKeyToContinue();
    }

    private void HandleStudy()
    {
        AnsiConsole.MarkupLine("Uh-oh- study time...");
        PressAnyKeyToContinue();
    }
    private void HandleViewPastSessions()
    {
        AnsiConsole.MarkupLine("Look at all these sessions!");
        PressAnyKeyToContinue();
    }

    private void HandleReports()
    {
        AnsiConsole.MarkupLine("Reporting for duty, sir!");
        PressAnyKeyToContinue();
    }


}
