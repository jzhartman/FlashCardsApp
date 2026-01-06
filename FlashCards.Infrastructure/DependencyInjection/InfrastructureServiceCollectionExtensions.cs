using FlashCards.Application.Interfaces;
using FlashCards.Infrastructure.Dapper;
using FlashCards.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace FlashCards.Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        // Register IDbConnection Factory
        services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

        // Register Dapper Wrapper
        services.AddScoped<IDapperWrapper, DapperWrapper>();

        // Register all repositories
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IStackRepository, StackRepository>();

        return services;
    }
}
