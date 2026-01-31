using FlashCards.Application.DTOs;
using FlashCards.Application.UseCases.Cards;
using FlashCards.Application.UseCases.StudySessions;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using FlashCards.Core.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.ConsoleUI.Handlers;

public class StudySessionService
{
    private readonly IServiceProvider _provider;
    private readonly IConsoleInput _input;
    private readonly IConsoleOutput _output;

    private StackResponse CurrentStack;

    public StudySessionService(IServiceProvider provider, IConsoleInput input, IConsoleOutput output)
    {
        _provider = provider;
        _input = input;
        _output = output;
    }

    public void Run(string stackName)
    {
        _output.PrintPageTitle("STUDY MODE");

        var cardsCorrect = new List<int>();
        var cardsIncorrect = new List<int>();
        var cards = GetAllCardsInStack(stackName);

        if (cards.Count < 1)
        {
            _output.PrintValidationErrorsFromCollection(new List<Error> { Errors.StackEmpty });
            _input.PressAnyKeyToContinue();
            return;
        }

        cards = ShuffleStack(cards);

        var session = StudyCards(stackName, cards, cardsCorrect, cardsIncorrect);
        AddSession(session);
        UpdateCardCounters(cardsCorrect, cardsIncorrect);
        _output.PrintSessionResults(session);

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

    private void UpdateCardCounters(List<int> cardsCorrect, List<int> cardsIncorrect)
    {
        var handler = _provider.GetRequiredService<UpdateCardCounterHandler>();
        handler.Handle(cardsCorrect, cardsIncorrect);
    }
    private void AddSession(StudySessionResponse session)
    {
        var handler = _provider.GetRequiredService<AddStudySessionHandler>();
        handler.Handle(session);
    }
    private List<CardResponse> GetAllCardsInStack(string stackName)
    {
        var handler = _provider.GetRequiredService<GetAllCardsByStackName>();
        return new List<CardResponse>(); //handler.Handle(stackName);
    }

    private List<CardResponse> ShuffleStack(List<CardResponse> cards)
    {
        CardResponse[] cardsArray = cards.ToArray();
        Random.Shared.Shuffle(cardsArray);
        return cardsArray.ToList();
    }
}
