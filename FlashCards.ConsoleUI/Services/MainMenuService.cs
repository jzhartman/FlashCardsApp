using FlashCards.Application.DTOs;
using FlashCards.Application.UseCases.Stacks;
using FlashCards.Application.UseCases.StudySessions;
using FlashCards.ConsoleUI.Enums;
using FlashCards.ConsoleUI.Handlers;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using FlashCards.ConsoleUI.Views;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Controllers;

public class MainMenuService
{
    private readonly IServiceProvider _provider;
    private readonly IConsoleInput _input;
    private readonly IConsoleOutput _output;
    private readonly StudySessionViewHandler _studySessionHandler;
    private readonly ReviewStackMenuHandler _stackMenu;

    private readonly MainMenuView _mainMenu;

    public MainMenuService(IServiceProvider provider, ReviewStackMenuHandler stackMenu, StudySessionViewHandler studySessionHandler,
                            IConsoleInput input, IConsoleOutput output,
                            MainMenuView mainMenu)
    {
        _provider = provider;
        _stackMenu = stackMenu;
        _input = input;
        _output = output;
        _studySessionHandler = studySessionHandler;

        _mainMenu = mainMenu;
    }
    public void Run()
    {
        bool exitApp = false;

        while (exitApp == false)
        {
            _output.PrintPageTitle("MAIN MENU");

            var stacks = GetAllStackNamesAndCardCounts();
            _output.PrintStackList(stacks);

            var selection = _mainMenu.Render();
            exitApp = HandleUserSelection(selection, stacks);
        }
    }

    private List<StackNameAndCardCountResponse> GetAllStackNamesAndCardCounts()
    {
        var handler = _provider.GetRequiredService<GetAllStackNamesAndCardCountsHandler>();
        return handler.Handle();
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

    private bool HandleUserSelection(MainMenuItem selection, List<StackNameAndCardCountResponse> stacks)
    {
        switch (selection)
        {
            case MainMenuItem.Review: HandleReviewCardsInStack(stacks); break;
            case MainMenuItem.Create: HandleCreateStack(); break;
            case MainMenuItem.Delete: HandleDeleteStack(stacks); break;
            case MainMenuItem.Study: HandleStudy(stacks); break;
            case MainMenuItem.Past: HandleViewPastSessions(stacks); break;
            case MainMenuItem.Report: HandleReports(); break;
            case MainMenuItem.Exit: return true;
            default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
        }

        return false;
    }

    private void HandleReviewCardsInStack(List<StackNameAndCardCountResponse> stacks)
    {
        var message = "Please enter the [yellow]ID[/] of the stack you wish to review:";
        int id = _input.GetRecordIdFromUser(message, 1, stacks.Count);
        _stackMenu.Run(stacks[id - 1].Name);
    }
    private void HandleCreateStack()
    {
        bool stackNameValid = false;

        while (stackNameValid == false)
        {
            var input = _input.GetTextInputFromUser("Enter stack name");

            var handler = _provider.GetRequiredService<AddStackHandler>();
            var result = handler.Handle(input);

            if (result.IsFailure) _output.PrintValidationErrorsFromCollection(result.Errors);

            else
            {
                _output.PrintSuccessMessage($"Created stack {result.Value}!");
                stackNameValid = true;
            }
        }
        _input.PressAnyKeyToContinue();
    }
    private void HandleDeleteStack(List<StackNameAndCardCountResponse> stacks)
    {
        var message = "Please enter the [yellow]ID[/] of the stack you wish to delete:";
        int id = _input.GetRecordIdFromUser(message, 1, stacks.Count);
        var stack = stacks[id - 1];

        if (_input.GetDeleteStackConfirmationFromUser(stack.Name, stack.CardCount))
        {
            var handler = _provider.GetRequiredService<DeleteStackByNameHandler>();
            handler.Handle(stack);
            _output.PrintSuccessMessage($"Deleted [green]{stack.Name}[/] stack!");
        }
        else _output.PrintCancellationMessage("deletion", $"{stack.Name} stack");

        _input.PressAnyKeyToContinue();
    }
    private void HandleStudy(List<StackNameAndCardCountResponse> stacks)
    {
        var message = "Please enter the [yellow]ID[/] of the stack you wish to study:";
        int id = _input.GetRecordIdFromUser(message, 1, stacks.Count);
        _studySessionHandler.Run(stacks[id - 1].Name);
    }
    private void HandleViewPastSessions(List<StackNameAndCardCountResponse> stacks)
    {
        var message = $"Either enter the [yellow]ID[/] of the stack whose sessions you wish to view, or enter \"0\" to view all past sessions:";
        int id = _input.GetRecordIdFromUser(message, 0, stacks.Count);

        if (id == 0)
        {
            var handler = _provider.GetRequiredService<GetAllStudySessionsHandler>();
            var sessions = handler.Handle();
            _output.PrintResultsForAllSessions(sessions);
        }
        else
        {
            var handler = _provider.GetRequiredService<GetStudySessionByIdHandler>();
            var session = handler.Handle(stacks[id - 1]);
            _output.PrintSessionResults(session);
        }
        _input.PressAnyKeyToContinue();
    }


    //
    // NOT YET IMPLEMENTED
    //




    private void HandleReports()
    {
        AnsiConsole.MarkupLine("Reporting for duty, sir!");
        _input.PressAnyKeyToContinue();
    }


}
