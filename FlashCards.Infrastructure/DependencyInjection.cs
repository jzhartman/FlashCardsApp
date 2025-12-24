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
        services.AddScoped< IDbConnection>(sp => new SqlConnection(connectionString));

        // TODO: Register all repositories
        //services.AddScoped<IRepositoryName, RepositoryName>();

        return services;
    }
}
