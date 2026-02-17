using FlashCards.Application.Interfaces;
using FlashCards.Infrastructure.Dapper;
using FlashCards.Infrastructure.Initialization;
using FlashCards.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace FlashCards.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                         IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        // Register IDbConnection Factory
        services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

        services.AddScoped<DbInitializer>(sp => new DbInitializer(connectionString));

        // Register Dapper Wrapper
        services.AddScoped<IDapperWrapper, DapperWrapper>();

        // Register all repositories
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IStackRepository, StackRepository>();
        services.AddScoped<IStudySessionRepository, StudySessionRepository>();

        return services;
    }
}
