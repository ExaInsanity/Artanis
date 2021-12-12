namespace Artanis;

using System;
using System.Threading.Tasks;

using Artanis.Configuration;
using Artanis.Initialization;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

public class Program
{
    private static ArtanisGlobalConfiguration config = null!;

    public static async Task Main(String[] argv)
    {
        IHostBuilder hostBuilder = Host.CreateDefaultBuilder().UseConsoleLifetime();

        config = new();
        await config.Load();

        ConfigureServices(hostBuilder);

        IHost host = hostBuilder.Build();

        await host.RunAsync();
    }

    private static void ConfigureServices(IHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders()
                .AddSerilog();
        })
            .ConfigureServices(services =>
            {
                services.AddSingleton<ArtanisGlobalConfiguration>()
                    .AddSingleton<ArtanisShardConfigurationHandler>()
                    .AddSingleton<ArtanisGuildConfigurationHandler>();

                services.InitializeGateway(config);
                services.RegisterCommands(config);
            });
    }
}
