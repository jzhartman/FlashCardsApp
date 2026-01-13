using FlashCards.Application.UseCases.Cards;
using FlashCards.Application.UseCases.Decks;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AddDeckHandler>();
        services.AddScoped<GetAllDecksHandler>();

        services.AddScoped<AddCardHandler>();


        return services;
    }
}
