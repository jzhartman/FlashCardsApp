using FlashCards.Application.Stacks.Add;
using FlashCards.Application.Stacks.Delete;
using FlashCards.Application.Stacks.GetAll;
using FlashCards.Application.StudySessions.GetAll;
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
    private readonly StudySessionService _studySessionService;
    private readonly ReviewStackMenuService _stackMenu;

    private readonly GetAllStacksWithCountsHandler _getAllStackNamesAndCounts;
    private readonly AddStackHandler _addStack;
    private readonly DeleteByIdHandler _deleteStack;
    private readonly GetAllStudySessionsHandler _getAllStudySessions;

    private readonly MainMenuView _menu;
    private readonly StackListView _stackList;
    private readonly StudySessionListView _studySessionList;

    public MainMenuService(ReviewStackMenuService stackMenu, StudySessionService studySessionService,
                            IConsoleInput input, IConsoleOutput output, MainMenuView menu, StackListView stackList, StudySessionListView studySessionList,
                            GetAllStacksWithCountsHandler getAllStackNamesAndCounts, AddStackHandler addStack,
                            DeleteByIdHandler deleteStack, GetAllStudySessionsHandler getAllStudySessions)
    {
        _stackMenu = stackMenu;
        _input = input;
        _output = output;
        _studySessionService = studySessionService;

        _menu = menu;
        _stackList = stackList;
        _studySessionList = studySessionList;

        _getAllStackNamesAndCounts = getAllStackNamesAndCounts;
        _addStack = addStack;
        _deleteStack = deleteStack;
        _getAllStudySessions = getAllStudySessions;
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
    private void HandleReviewStack(List<StackNamesWithCountsResponse> stacks)
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
    private void HandleStudy(List<StackNamesWithCountsResponse> stacks)
    {
        var message = "Please enter the [yellow]ID[/] of the stack you wish to study:";
        int id = _input.GetRecordIdFromUser(message, 1, stacks.Count);
        _studySessionService.Run(stacks[id - 1]);
    }
    private void HandleViewPastSessions(List<StackNamesWithCountsResponse> stacks)
    {
        var result = _getAllStudySessions.Handle();

        if (result.IsFailure)
        {
            _output.PrintValidationErrorsFromCollection(new List<Error> { Errors.NoStudySessions });
            _input.PressAnyKeyToContinue();
        }
        else
        {
            RenderPastSessionList(result.Value);
        }
    }
    private void RenderPastSessionList(List<StudySessionResponse> sessions)
    {
        bool continueReview = true;
        ConsoleKeyInfo keyInfo;
        int i = 0;

        while (continueReview)
        {
            _output.PrintPageTitle("REVIEW STACK MENU");
            _studySessionList.Render(sessions);
            _output.PrintStudySessionKeypressOptions();

            bool validKey = false;

            while (validKey == false)
            {
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        sessions = sessions.OrderBy(s => s.StackName).ToList();
                        validKey = true;
                        break;
                    case ConsoleKey.LeftArrow:
                        sessions = sessions.OrderBy(s => s.SessionDate).ToList();
                        validKey = true;
                        break;
                    case ConsoleKey.RightArrow:
                        sessions = sessions.OrderBy(s => s.Score).ToList();
                        validKey = true;
                        break;
                    case ConsoleKey.Escape:
                        validKey = true;
                        continueReview = false;
                        break;
                    default:
                        Console.WriteLine($"Invalid keypress -- see options");
                        break;
                }
            }

        }
    }


    //
    // NOT YET IMPLEMENTED
    //




    private void HandleReports()
    {
        // Print first report
        // Print second report
        // Print input key options for user
        //      - 
        AnsiConsole.MarkupLine("Reporting for duty, sir!");
        _input.PressAnyKeyToContinue();
    }


}
