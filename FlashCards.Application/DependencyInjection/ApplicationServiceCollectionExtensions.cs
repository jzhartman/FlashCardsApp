using FlashCards.Application.UseCases.Cards;
using FlashCards.Application.UseCases.Stacks;
using FlashCards.Application.UseCases.StudySessions;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
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
        services.AddScoped<EditCardFrontTextHandler>();
        services.AddScoped<EditCardHandler>();
        services.AddScoped<UpdateCardCounterHandler>();

        services.AddScoped<AddStudySessionHandler>();

        return services;
    }
}
