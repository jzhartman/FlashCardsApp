using FlashCards.Application.DTOs;
using FlashCards.Application.Interfaces;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using FlashCards.ConsoleUI.Views;

namespace FlashCards.ConsoleUI.Handlers;

public class StudySessionService
{
    private readonly IServiceProvider _provider;
    private readonly IConsoleInput _input;
    private readonly IConsoleOutput _output;

    private readonly IGetAllCardsByStackName _getAllCardsByStackName;
    private readonly IAddStudySessionHandler _addStudySessionHandler;
    private readonly IUpdateCardCounterHandler _updateCardCounterHandler;

    private readonly StudySessionListView _studySessionList;

    private StackResponse CurrentStack;

    public StudySessionService(IServiceProvider provider, IConsoleInput input, IConsoleOutput output,
                                IGetAllCardsByStackName getAllCardsByStackName, IAddStudySessionHandler addStudySessionHandler,
                                IUpdateCardCounterHandler updateCardCounterHandler, StudySessionListView studySessionList)
    {
        _provider = provider;
        _input = input;
        _output = output;
        _getAllCardsByStackName = getAllCardsByStackName;
        _addStudySessionHandler = addStudySessionHandler;
        _updateCardCounterHandler = updateCardCounterHandler;
        _studySessionList = studySessionList;
    }

    public void Run(string stackName)
    {
        _output.PrintPageTitle("STUDY MODE");

        var cardsCorrect = new List<int>();
        var cardsIncorrect = new List<int>();


        var result = _getAllCardsByStackName.Handle(stackName);

        if (result.IsFailure)
        {
            _output.PrintValidationErrorsFromCollection(result.Errors);

        }
        else
        {
            var cards = result.Value;

            cards = ShuffleStack(cards);

            var session = StudyCards(stackName, cards, cardsCorrect, cardsIncorrect);
            _addStudySessionHandler.Handle(session);
            _updateCardCounterHandler.Handle(cardsCorrect, cardsIncorrect);
            _studySessionList.Render(session);
        }

        _input.PressAnyKeyToContinue();
    }

    private StudySessionResponse StudyCards(string stackName, List<CardResponse> cards, List<int> cardsCorrect, List<int> cardsIncorrect)
    {
        int cardsStudied = 0;

        foreach (var card in cards)
        {
            _output.PrintPageTitle("STUDY MODE");

            _output.PrintCardTextInPanel(card.FrontText);
            _input.PressAnyKeyToContinue(1, "Press any key to flip card...");
            _output.PrintCardTextInPanel(card.BackText);
            bool correctAnswer = _input.GetPassStateFromUser();

            if (correctAnswer)
            {
                Console.WriteLine("Hooray, you did it!");
                cardsCorrect.Add(card.Id);
            }
            else
            {
                Console.WriteLine("Opps, that wasn't quite right...");
                cardsIncorrect.Add(card.Id);
            }

            cardsStudied++;
            if (_input.ContinueStudyMode() == false) break;
        }

        double score = (double)cardsCorrect.Count / cardsStudied * 100;
        return new StudySessionResponse(DateTime.Now, stackName, score, cardsStudied, cardsCorrect.Count, cardsIncorrect.Count);
    }

    private List<CardResponse> ShuffleStack(List<CardResponse> cards)
    {
        CardResponse[] cardsArray = cards.ToArray();
        Random.Shared.Shuffle(cardsArray);
        return cardsArray.ToList();
    }
}
