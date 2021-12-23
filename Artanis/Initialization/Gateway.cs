namespace Artanis.Initialization;

using System;

using Artanis.Configuration;

using Microsoft.Extensions.DependencyInjection;

using Remora.Discord.API.Abstractions.Gateway.Commands;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.Caching.Extensions;
using Remora.Discord.Caching.Services;
using Remora.Discord.Commands.Extensions;
using Remora.Discord.Gateway;
using Remora.Discord.Gateway.Extensions;

internal static class Gateway
{
    public static void InitializeGateway(this IServiceCollection services, ArtanisGlobalConfiguration configuration)
    {
        services.AddDiscordCommands(true)
            .AddDiscordGateway(xm => configuration.Token);

        services.Configure<DiscordGatewayClientOptions>(gw =>
        {
            gw.Intents = GatewayIntents.Guilds |
                GatewayIntents.GuildMessages;
        });

        services.AddDiscordCaching();

        services.Configure<CacheSettings>(settings =>
        {
            settings.SetDefaultAbsoluteExpiration(TimeSpan.Parse(configuration
                .Value<String>("artanis.caching.expiration.default.absolute")));
            settings.SetDefaultSlidingExpiration(TimeSpan.Parse(configuration
                .Value<String>("artanis.caching.expiration.default.sliding")));

            settings.SetAbsoluteExpiration<IGuild>(TimeSpan.Parse(configuration
                .Value<String>("artanis.caching.expiration.guild.absolute")));
            settings.SetSlidingExpiration<IGuild>(TimeSpan.Parse(configuration
                .Value<String>("artanis.caching.expiration.guild.sliding")));

            settings.SetAbsoluteExpiration<IGuildMember>(TimeSpan.Parse(configuration
                .Value<String>("artanis.caching.expiration.guild.member.absolute")));
            settings.SetSlidingExpiration<IGuildMember>(TimeSpan.Parse(configuration
                .Value<String>("artanis.caching.expiration.guild.member.sliding")));

            settings.SetAbsoluteExpiration<IChannel>(TimeSpan.Parse(configuration
                .Value<String>("artanis.caching.expiration.channel.absolute")));
            settings.SetAbsoluteExpiration<IChannel>(TimeSpan.Parse(configuration
                .Value<String>("artanis.caching.expiration.channel.sliding")));

            settings.SetAbsoluteExpiration<IUser>(TimeSpan.Parse(configuration
                .Value<String>("artanis.caching.expiration.user.absolute")));
            settings.SetAbsoluteExpiration<IUser>(TimeSpan.Parse(configuration
                .Value<String>("artanis.caching.expiration.user.sliding")));
        });
    }
}
