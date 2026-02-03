using FlashCards.Application.Interfaces;
using FlashCards.Application.UseCases.Cards;
using FlashCards.Application.UseCases.Stacks;
using FlashCards.Application.UseCases.StudySessions;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAddStackHandler, AddStackHandler>();
        services.AddScoped<IGetAllStackNamesAndCardCountsHandler, GetAllStackNamesAndCardCountsHandler>();
        services.AddScoped<IDeleteStackByNameHandler, DeleteStackByNameHandler>();

        services.AddScoped<IAddCardHandler, AddCardHandler>();
        services.AddScoped<IGetAllCardsByStackName, GetAllCardsByStackName>();
        services.AddScoped<IDeleteCardByIdHandler, DeleteCardByIdHandler>();
        services.AddScoped<IGetCardTextHandler, GetCardTextHandler>();
        services.AddScoped<IEditCardTextHandler, EditCardTextBySideHandler>();
        services.AddScoped<IEditCardHandler, EditCardHandler>();
        services.AddScoped<IUpdateCardCounterHandler, UpdateCardCounterHandler>();

        services.AddScoped<IAddStudySessionHandler, AddStudySessionHandler>();
        services.AddScoped<IGetAllStudySessionsHandler, GetAllStudySessionsHandler>();
        services.AddScoped<IGetStudySessionByIdHandler, GetStudySessionByIdHandler>();

        return services;
    }
}
