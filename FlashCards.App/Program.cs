using FlashCards.Application;
using FlashCards.ConsoleUI;
using FlashCards.ConsoleUI.Controllers;
using FlashCards.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlashCards.App;

internal class Program
{
    static void Main(string[] args)
    {
        var isDeveloperMode = true;

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var services = new ServiceCollection();
        services.AddApplication();
        services.AddInfrastructure(config, isDeveloperMode);
        services.AddConsoleUI();

        var provider = services.BuildServiceProvider();
        var mainMenu = provider.GetRequiredService<MainMenuService>();
        mainMenu.Run();
    }
}
