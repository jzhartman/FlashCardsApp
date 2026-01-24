using FlashCards.ConsoleUI.Controllers;
using FlashCards.ConsoleUI.Handlers;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.ConsoleUI.DependencyInjection;

public static class ConsoleUIServiceCollectionExtensions
{
    public static IServiceCollection AddConsoleUI(this IServiceCollection services)
    {
        services.AddScoped<MainMenuHandler>();
        services.AddScoped<ReviewStackMenuHandler>();
        services.AddScoped<StudySessionViewHandler>();
        services.AddScoped<IConsoleInput, ConsoleInput>();
        services.AddScoped<IConsoleOutput, ConsoleOutput>();

        return services;
    }
}
