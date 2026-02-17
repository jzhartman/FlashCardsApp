using FlashCards.Application.Cards;
using FlashCards.Application.Cards.GetAllByStackId;
using FlashCards.Application.Cards.UpdateCardCounters;
using FlashCards.Application.Stacks.GetAll;
using FlashCards.Application.StudySessions.Add;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using FlashCards.ConsoleUI.Views;

namespace FlashCards.ConsoleUI.Handlers;

public class StudySessionService
{
    private readonly IServiceProvider _provider;
    private readonly ConsoleInput _input;
    private readonly ConsoleOutput _output;

    private readonly GetAllByStackId _getAllByStackId;
    private readonly AddStudySessionHandler _addStudySessionHandler;
    private readonly UpdateCardCountersHandler _updateCardCounterHandler;

    private readonly StudySessionView _studySession;

    public StudySessionService(IServiceProvider provider, ConsoleInput input, ConsoleOutput output,
                                GetAllByStackId getAllByStackId, AddStudySessionHandler addStudySessionHandler,
                                UpdateCardCountersHandler updateCardCounterHandler, StudySessionView studySession)
    {
        _provider = provider;
        _input = input;
        _output = output;
        _getAllByStackId = getAllByStackId;
        _addStudySessionHandler = addStudySessionHandler;
        _updateCardCounterHandler = updateCardCounterHandler;
        _studySession = studySession;
    }

    public void Run(StackNamesWithCountsResponse stack)
    {
        _output.PrintPageTitle("STUDY MODE");

        var cardsCorrect = new List<int>();
        var cardsIncorrect = new List<int>();


        var result = _getAllByStackId.Handle(stack.Id);

        if (result.IsFailure)
        {
            _output.PrintValidationErrorsFromCollection(result.Errors);

        }
        else
        {
            var cards = result.Value;

            cards = ShuffleStack(cards);

            var session = StudyCards(stack.Id, stack.Name, cards, cardsCorrect, cardsIncorrect);
            _addStudySessionHandler.Handle(session);
            _updateCardCounterHandler.Handle(cardsCorrect, cardsIncorrect);
            _studySession.Render(session);
        }

        _input.PressAnyKeyToContinue();
    }

    private AddStudySessionCommand StudyCards(int stackId, string stackName, List<CardResponse> cards, List<int> cardsCorrect, List<int> cardsIncorrect)
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
                Console.WriteLine("Congratulations!");
                cardsCorrect.Add(card.Id);
            }
            else
            {
                Console.WriteLine("Too bad! Keep studying!");
                cardsIncorrect.Add(card.Id);
            }

            cardsStudied++;
            if (cardsStudied == cards.Count) break;
            if (_input.ContinueStudyMode() == false) break;
        }

        double score = (double)cardsCorrect.Count / cardsStudied * 100;
        return new AddStudySessionCommand(stackId, stackName, DateTime.Now, score, cardsStudied, cardsCorrect.Count, cardsIncorrect.Count);
    }

    private List<CardResponse> ShuffleStack(List<CardResponse> cards)
    {
        CardResponse[] cardsArray = cards.ToArray();
        Random.Shared.Shuffle(cardsArray);
        return cardsArray.ToList();
    }
}
