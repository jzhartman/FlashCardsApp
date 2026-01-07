using FlashCards.Application.Services;
using FlashCards.Application.UseCases.Stacks;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AddStackHandler>();
        services.AddScoped<GetStacksHandler>();
        services.AddScoped<StackNameUniquenessService>();


        return services;
    }
}
