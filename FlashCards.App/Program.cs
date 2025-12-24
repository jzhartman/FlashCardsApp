using FlashCards.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.App;

internal class Program
{
    static void Main(string[] args)
    {

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var services = new ServiceCollection();

        services.AddInfrastructure(config);

        var provider = services.BuildServiceProvider();

        // Repos
        // var myRepo = provider.GetRequiredService<IMyRepo>();

    }
}
