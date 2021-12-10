namespace Artanis.Initialization;

using Artanis.Configuration;

using Microsoft.Extensions.DependencyInjection;

using Remora.Discord.Caching.Extensions;
using Remora.Discord.Caching.Services;
using Remora.Discord.Commands.Extensions;
using Remora.Discord.Gateway.Extensions;

internal static class Gateway
{
    public static void InitializeGateway(this IServiceCollection services, ArtanisGlobalConfiguration configuration)
    {
        services.AddDiscordCommands(true, false, false)
            .AddDiscordGateway(xm => configuration.Token);

        services.AddDiscordCaching();

        services.Configure<CacheSettings>(settings =>
        {

        });
    }
}
