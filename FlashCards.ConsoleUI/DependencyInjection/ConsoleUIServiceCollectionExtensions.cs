using FlashCards.ConsoleUI.Controllers;
using FlashCards.ConsoleUI.Handlers;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using FlashCards.ConsoleUI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.ConsoleUI.DependencyInjection;

public static class ConsoleUIServiceCollectionExtensions
{
    public static IServiceCollection AddConsoleUI(this IServiceCollection services)
    {
        services.AddScoped<MainMenuService>();
        services.AddScoped<ReviewStackMenuService>();
        services.AddScoped<StudySessionService>();
        services.AddScoped<IConsoleInput, ConsoleInput>();
        services.AddScoped<IConsoleOutput, ConsoleOutput>();

        services.AddScoped<MainMenuView>();
        services.AddScoped<ReviewStackMenuView>();
        services.AddScoped<StackListView>();

        return services;
    }
}
