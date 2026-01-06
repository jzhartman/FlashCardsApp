using FlashCards.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<StackHandler>();

        return services;
    }
}
