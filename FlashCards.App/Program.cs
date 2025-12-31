using FlashCards.ConsoleUI.Controllers;
using FlashCards.ConsoleUI.DependencyInjection;
using FlashCards.Infrastructure.DependencyInjection;
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
        services.AddConsoleUI();

        var provider = services.BuildServiceProvider();

        // Repos
        // var myRepo = provider.GetRequiredService<IMyRepo>();

        var mainMenu = provider.GetRequiredService<MainMenuHandler>();
        mainMenu.Run();
    }
}
