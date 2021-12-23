namespace Artanis.Initialization;

using Artanis.Commands;
using Artanis.Configuration;

using Microsoft.Extensions.DependencyInjection;

using Remora.Commands.Extensions;

internal static class Commands
{
    public static void RegisterCommands(this IServiceCollection services, ArtanisGlobalConfiguration config)
    {
        services.AddCommandGroup<MuteCommand>();
    }
}
