using FlashCards.Application;
using FlashCards.ConsoleUI;
using FlashCards.ConsoleUI.Controllers;
using FlashCards.Infrastructure;
using FlashCards.Infrastructure.Initialization;
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
        services.AddApplication();
        services.AddInfrastructure(config);
        services.AddConsoleUI();

        var provider = services.BuildServiceProvider();
        var initializer = provider.GetRequiredService<DbInitializer>();
        initializer.Initialize();

        var mainMenu = provider.GetRequiredService<MainMenuService>();
        mainMenu.Run();
    }
}
