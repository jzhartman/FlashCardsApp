using FlashCards.Application.DTOs;
using FlashCards.Application.UseCases.Stacks;
using FlashCards.Application.UseCases.StudySessions;
using FlashCards.ConsoleUI.Enums;
using FlashCards.ConsoleUI.Handlers;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using FlashCards.ConsoleUI.Views;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Controllers;

public class MainMenuService
{
    private readonly IConsoleInput _input;
    private readonly IConsoleOutput _output;
    private readonly StudySessionViewHandler _studySessionHandler;
    private readonly ReviewStackMenuService _stackMenu;

    private readonly IGetAllStackNamesAndCardCountsHandler _getAllStackNamesAndCardCounts;
    private readonly IAddStackHandler _addStack;
    private readonly IDeleteStackByNameHandler _deleteStack;
    private readonly IGetAllStudySessionsHandler _getAllStudySessions;
    private readonly IGetStudySessionByIdHandler _getStudySessionById;

    private readonly MainMenuView _menu;

    public MainMenuService(ReviewStackMenuService stackMenu, StudySessionViewHandler studySessionHandler,
                            IConsoleInput input, IConsoleOutput output, MainMenuView menu,
                            IGetAllStackNamesAndCardCountsHandler getAllStackNamesAndCardCounts, IAddStackHandler addStack,
                            IDeleteStackByNameHandler deleteStack, IGetAllStudySessionsHandler getAllStudySessions,
                            IGetStudySessionByIdHandler getStudySessionById)
    {
        _stackMenu = stackMenu;
        _input = input;
        _output = output;
        _studySessionHandler = studySessionHandler;

        _menu = menu;

        _getAllStackNamesAndCardCounts = getAllStackNamesAndCardCounts;
        _addStack = addStack;
        _deleteStack = deleteStack;
        _getAllStudySessions = getAllStudySessions;
        _getStudySessionById = getStudySessionById;
    }
    public void Run()
    {
        bool exitApp = false;

        while (exitApp == false)
        {
            _output.PrintPageTitle("MAIN MENU");

            var stacks = _getAllStackNamesAndCardCounts.Handle();
            _output.PrintStackList(stacks);

            var selection = _menu.Render();

            switch (selection)
            {
                case MainMenuItem.ReviewStack: HandleReviewStack(stacks); break;
                case MainMenuItem.CreateStack: HandleCreateStack(); break;
                case MainMenuItem.DeleteStack: HandleDeleteStack(stacks); break;
                case MainMenuItem.StudyStack: HandleStudy(stacks); break;
                case MainMenuItem.ViewPastSessions: HandleViewPastSessions(stacks); break;
                case MainMenuItem.Report: HandleReports(); break;
                case MainMenuItem.Exit: exitApp = true; break;
                default: AnsiConsole.Markup("[bold red]ERROR:[/] Invalid input!"); break;
            }
        }
    }

    private void HandleReviewStack(List<StackNameAndCardCountResponse> stacks)
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

            var result = _addStack.Handle(input);

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
            _deleteStack.Handle(stack);
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
            var sessions = _getAllStudySessions.Handle();
            _output.PrintResultsForAllSessions(sessions);
        }
        else
        {
            var session = _getStudySessionById.Handle(stacks[id - 1]);
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
