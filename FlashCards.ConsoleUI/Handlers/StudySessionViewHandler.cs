using FlashCards.Application.DTOs;
using FlashCards.Application.UseCases.Cards;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.ConsoleUI.Handlers;

public class StudySessionViewHandler
{
    private readonly IServiceProvider _provider;
    private readonly IConsoleInput _input;
    private readonly IConsoleOutput _output;

    private StackResponse CurrentStack;

    public StudySessionViewHandler(IServiceProvider provider, IConsoleInput input, IConsoleOutput output)
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
        cards = ShuffleStack(cards);

        StudyCards(stackName, cards, cardsCorrect, cardsIncorrect);
    }

    private void StudyCards(string stackName, List<CardResponse> cards, List<int> cardsCorrect, List<int> cardsIncorrect)
    {
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

            if (_input.ContinueStudyMode() == false) break;
        }

        _output.PrintPageTitle("STUDY MODE");
        _output.PrintSessionResults(stackName, cards.Count, cardsCorrect.Count, cardsIncorrect.Count);
        _input.PressAnyKeyToContinue();
    }



    private List<CardResponse> GetAllCardsInStack(string stackName)
    {
        var handler = _provider.GetRequiredService<GetAllCardsByStackName>();
        return handler.Handle(stackName);
    }

    private List<CardResponse> ShuffleStack(List<CardResponse> cards)
    {
        CardResponse[] cardsArray = cards.ToArray();
        Random.Shared.Shuffle(cardsArray);
        return cardsArray.ToList();
    }
}
