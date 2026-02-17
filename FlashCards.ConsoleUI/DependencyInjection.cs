using FlashCards.ConsoleUI.Controllers;
using FlashCards.ConsoleUI.Handlers;
using FlashCards.ConsoleUI.Input;
using FlashCards.ConsoleUI.Output;
using FlashCards.ConsoleUI.Services;
using FlashCards.ConsoleUI.Views;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.ConsoleUI;

public static class DependencyInjection
{
    public static IServiceCollection AddConsoleUI(this IServiceCollection services)
    {
        services.AddScoped<MainMenuService>();
        services.AddScoped<ReviewStackMenuService>();
        services.AddScoped<StudySessionService>();
        services.AddScoped<ReportService>();
        services.AddScoped<ConsoleInput>();
        services.AddScoped<ConsoleOutput>();

        services.AddScoped<MainMenuView>();
        services.AddScoped<ReviewStackMenuView>();
        services.AddScoped<StackListView>();
        services.AddScoped<StudySessionListView>();
        services.AddScoped<StudySessionView>();
        services.AddScoped<CardListView>();
        services.AddScoped<AverageScorePerMonthView>();
        services.AddScoped<SessionCountPerMonthView>();
        services.AddScoped<SessionYearSelectionView>();

        return services;
    }
}
