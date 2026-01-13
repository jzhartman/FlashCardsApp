using FlashCards.ConsoleUI.Controllers;
using FlashCards.ConsoleUI.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.ConsoleUI.DependencyInjection;

public static class ConsoleUIServiceCollectionExtensions
{
    public static IServiceCollection AddConsoleUI(this IServiceCollection services)
    {
        services.AddScoped<MainMenuHandler>();
        services.AddScoped<DeckMenuHandler>();
        services.AddScoped<StudyMenuHandler>();
        services.AddScoped<ViewDeckMenuHandler>();

        return services;
    }
}
