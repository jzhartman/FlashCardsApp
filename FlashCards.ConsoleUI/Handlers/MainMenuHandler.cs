using FlashCards.Application.DTOs;
using FlashCards.Application.UseCases.Stacks;
using FlashCards.ConsoleUI.Handlers;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Controllers;

public class MainMenuHandler
{
    private readonly IServiceProvider _provider;
    private readonly IConsoleInput _input;
    private readonly IConsoleOutput _output;
    private readonly ReviewStackMenuHandler _stackMenu;

    public MainMenuHandler(IServiceProvider provider, ReviewStackMenuHandler stackMenu,
                            IConsoleInput input, IConsoleOutput output)
    {
        _provider = provider;
        _stackMenu = stackMenu;
        _input = input;
        _output = output;
    }
    public void Run()
    {
        bool exitApp = false;

        while (exitApp == false)
        {
            _output.PrintPageTitle("MAIN MENU");

            var stacks = GetAllStackNamesAndCardCounts();
            _output.PrintStackList(stacks);

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
            case "Review Cards in Stack": HandleReviewCardsInStack(stacks); break;
            case "Create New Stack": HandleCreateStack(); break;
            case "Delete Stack": HandleDeleteStack(stacks); break;
            case "Begin Study Session": HandleStudy(); break;
            case "View Past Study Sessions": HandleViewPastSessions(); break;
            case "View Reports": HandleReports(); break;
            case "Exit": return true;
            default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
        }

        return false;
    }


    private void HandleReviewCardsInStack(List<StackNameAndCardCountResponse> stacks)
    {
        int id = _input.GetRecordIdFromUser("review", 1, stacks.Count);
        _stackMenu.Run(stacks[id - 1].Name);
    }

    private List<StackNameAndCardCountResponse> GetAllStackNamesAndCardCounts()
    {
        var handler = _provider.GetRequiredService<GetAllStackNamesAndCardCountsHandler>();
        return handler.Handle();
    }

    private StackNameAndCardCountResponse GetStackSelectionFromUser(List<StackNameAndCardCountResponse> stacks, string action)
    {
        int id = _input.GetRecordIdFromUser(action, 1, stacks.Count);


        return stacks[id - 1];
    }

    private void HandleCreateStack()
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

        else AnsiConsole.WriteLine($"Created stack {result.Value.Name}!");
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

    private void HandleDeleteStack(List<StackNameAndCardCountResponse> stacks)
    {
        var stack = GetStackSelectionFromUser(stacks, "delete");

        if (ConfirmDelete(stack))
        {
            var handler = _provider.GetRequiredService<DeleteStackByNameHandler>();
            handler.Handle(stack);
            Console.WriteLine("Card deleted!");
        }
        else Console.WriteLine("Cancelled delete!");

        PressAnyKeyToContinue();
    }

    private bool ConfirmDelete(StackNameAndCardCountResponse stack)
    {
        Console.WriteLine($"About to delete stack {stack.Name} and all {stack.CardCount} included cards.");
        Console.WriteLine();
        Console.Write("Enter y to delete or anything else to cancel: ");
        var input = Console.ReadLine();

        return input == "y" ? true : false;
    }



    //
    // NOT YET IMPLEMENTED
    //

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
