using FlashCards.Application.Cards;
using FlashCards.Application.Cards.Add;
using FlashCards.Application.Cards.Delete;
using FlashCards.Application.Cards.EditTextBySide;
using FlashCards.Application.Stacks.Add;
using FlashCards.Application.Stacks.Delete;
using FlashCards.Application.Stacks.GetAll;
using FlashCards.Application.StudySessions.Add;
using FlashCards.Application.StudySessions.GetAll;
using FlashCards.Application.StudySessions.GetByStackId;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AddStackHandler>();
        services.AddScoped<GetAllStackNamesAndCardCountsHandler>();
        services.AddScoped<DeleteStackByNameHandler>();

        services.AddScoped<AddCardHandler>();
        services.AddScoped<GetAllCardsByStackName>();
        services.AddScoped<DeleteCardByIdHandler>();
        services.AddScoped<GetCardTextHandler>();
        services.AddScoped<EditCardTextBySideHandler>();
        services.AddScoped<EditCardHandler>();
        services.AddScoped<UpdateCardCounterHandler>();

        services.AddScoped<AddStudySessionHandler>();
        services.AddScoped<GetAllStudySessionsHandler>();
        services.AddScoped<GetStudySessionsByStackIdHandler>();

        return services;
    }
}
