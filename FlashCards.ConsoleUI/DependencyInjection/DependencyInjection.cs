using FlashCards.ConsoleUI.Controllers;
using FlashCards.ConsoleUI.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.ConsoleUI.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddConsoleUI(this IServiceCollection services)
    {
        services.AddScoped<MainMenuHandler>();
        services.AddScoped<StackMenuHandler>();
        services.AddScoped<StudyMenuHandler>();
        services.AddScoped<ViewStackMenuHandler>();

        return services;
    }
}
