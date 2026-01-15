using FlashCards.Application.UseCases.Cards;
using FlashCards.Application.UseCases.Stacks;
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

        return services;
    }
}
