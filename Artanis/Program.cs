namespace Artanis;

using System;
using System.Threading.Tasks;

using Artanis.Configuration;
using Artanis.Initialization;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Remora.Discord.Gateway;

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

        await host.Services.GetRequiredService<DiscordGatewayClient>().RunAsync(new());

        await host.RunAsync();
    }

    private static void ConfigureServices(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
            {
                services.AddSingleton<ArtanisGlobalConfiguration>();

                services.InitializeGateway(config);
                services.RegisterCommands(config);
            });
    }
}
