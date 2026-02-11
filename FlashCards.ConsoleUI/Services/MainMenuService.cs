using FlashCards.Application.DTOs;
using FlashCards.Application.Stacks.Add;
using FlashCards.Application.Stacks.Delete;
using FlashCards.Application.Stacks.GetAll;
using FlashCards.Application.StudySessions.GetAll;
using FlashCards.Application.StudySessions.GetByStackId;
using FlashCards.ConsoleUI.Enums;
using FlashCards.ConsoleUI.Handlers;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using FlashCards.ConsoleUI.Views;
using FlashCards.Core.Validation;
using Spectre.Console;

namespace FlashCards.ConsoleUI.Controllers;

public class MainMenuService
{
    private readonly IConsoleInput _input;
    private readonly IConsoleOutput _output;
    private readonly StudySessionService _studySessionHandler;
    private readonly ReviewStackMenuService _stackMenu;

    private readonly GetAllStacksWithCountsHandler _getAllStackNamesAndCounts;
    private readonly AddStackHandler _addStack;
    private readonly DeleteByIdHandler _deleteStack;
    private readonly GetAllStudySessionsHandler _getAllStudySessions;
    private readonly GetStudySessionsByStackIdHandler _getStudySessionById;

    private readonly MainMenuView _menu;
    private readonly StackListView _stackList;
    private readonly StudySessionListView _studySessionList;

    public MainMenuService(ReviewStackMenuService stackMenu, StudySessionService studySessionHandler,
                            IConsoleInput input, IConsoleOutput output, MainMenuView menu, StackListView stackList, StudySessionListView studySessionList,
                            GetAllStacksWithCountsHandler getAllStackNamesAndCounts, AddStackHandler addStack,
                            DeleteByIdHandler deleteStack, GetAllStudySessionsHandler getAllStudySessions,
                            GetStudySessionsByStackIdHandler getStudySessionById)
    {
        _stackMenu = stackMenu;
        _input = input;
        _output = output;
        _studySessionHandler = studySessionHandler;

        _menu = menu;
        _stackList = stackList;
        _studySessionList = studySessionList;

        _getAllStackNamesAndCounts = getAllStackNamesAndCounts;
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

            var stacks = GetStacks();

            _stackList.Render(stacks);

            MainMenuItem[] menuItems = Enum.GetValues<MainMenuItem>();

            if (stacks.Count <= 0) menuItems = new MainMenuItem[2] { MainMenuItem.CreateStack, MainMenuItem.Exit };

            var selection = _menu.Render(menuItems);

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

    private List<StackNamesWithCountsResponse> GetStacks()
    {
        var result = _getAllStackNamesAndCounts.Handle();

        if (result.IsFailure)
        {
            _output.PrintValidationErrorsFromCollection(result.Errors);
            return new List<StackNamesWithCountsResponse>();
        }
        else
        {
            return result.Value;
        }
    }
    private void HandleReviewStack(List<StackNamesWithCountsResponse> stacks)           // Follow to ReviewStackHandler
    {
        var message = "Please enter the [yellow]ID[/] of the stack you wish to review:";
        int id = _input.GetRecordIdFromUser(message, 1, stacks.Count);
        _stackMenu.Run(stacks[id - 1]);
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
    private void HandleDeleteStack(List<StackNamesWithCountsResponse> stacks)
    {
        var message = "Please enter the [yellow]ID[/] of the stack you wish to delete:";
        int id = _input.GetRecordIdFromUser(message, 1, stacks.Count);
        var stack = stacks[id - 1];

        if (_input.GetDeleteStackConfirmationFromUser(stack.Name, stack.CardCount, stack.SessionCount))
        {
            _deleteStack.Handle(stack);
            _output.PrintSuccessMessage($"Deleted [green]{stack.Name}[/] stack!");
        }
        else _output.PrintCancellationMessage("deletion", $"{stack.Name} stack");

        _input.PressAnyKeyToContinue();
    }
    private void HandleStudy(List<StackNamesWithCountsResponse> stacks)                 // Follow to StudySessionHandler
    {
        var message = "Please enter the [yellow]ID[/] of the stack you wish to study:";
        int id = _input.GetRecordIdFromUser(message, 1, stacks.Count);
        _studySessionHandler.Run(stacks[id - 1].Name);
    }
    private void HandleViewPastSessions(List<StackNamesWithCountsResponse> stacks)
    {
        var message = $"Either enter the [yellow]ID[/] of the stack whose sessions you wish to view, or enter \"0\" to view all past sessions:";
        int id = _input.GetRecordIdFromUser(message, 0, stacks.Count);
        var sessions = new List<StudySessionResponse>();

        if (id == 0)
            sessions = _getAllStudySessions.Handle();
        else
            sessions = _getStudySessionById.Handle(stacks[id - 1]);

        if (sessions == null || sessions.Count == 0)
            _output.PrintValidationErrorsFromCollection(new List<Error> { Errors.NoStudySessions });
        else
            _studySessionList.Render(sessions);

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
