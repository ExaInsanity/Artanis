namespace Artanis.Commands;

using System;
using System.Threading.Tasks;

using Artanis.Configuration;
using Artanis.Modlog;
using Artanis.Modlog.Reference;

using Remora.Commands.Attributes;
using Remora.Commands.Groups;
using Remora.Discord.API.Abstractions.Objects;
using Remora.Discord.API.Abstractions.Rest;
using Remora.Discord.Commands.Conditions;
using Remora.Discord.Commands.Contexts;
using Remora.Rest.Core;
using Remora.Results;

internal class MuteCommand : CommandGroup
{
    private readonly ICommandContext _context;
    private readonly IDiscordRestChannelAPI _channels;
    private readonly IDiscordRestGuildAPI _guild;
    private readonly ArtanisGlobalConfiguration _config;

    public MuteCommand(ICommandContext context,
        IDiscordRestChannelAPI channels,
        IDiscordRestGuildAPI guilds,
        ArtanisGlobalConfiguration config)
    {
        this._context = context;
        this._channels = channels;
        this._guild = guilds;
        this._config = config;
    }

    [Command("mute", "timeout")]
    [RequireContext(ChannelContext.Guild)]
    [RequireDiscordPermission(DiscordPermission.ManageChannels)]
    public async Task<Result> Mute(IGuildMember member, TimeSpan time, String reason)
    {
        Snowflake muterole = new(this._config.Value<UInt64>("artanis.roles.muterole"));

        if(reason == "usedefault")
        {
            reason = "No reason given.";
        }

        await this._guild.AddGuildMemberRoleAsync(this._context.GuildID.Value, member.User.Value.ID, muterole,
            new Optional<String>(reason));

        member.AddModlogEntry(new()
        {
            Reason = reason,
            Time = DateTime.UtcNow,
            Type = ModlogEntryType.mute
        });

        return Result.FromSuccess();
    }

    public async Task Mute(IGuildMember member, String reason) => await this.Mute(member, TimeSpan.MaxValue, reason);
}
