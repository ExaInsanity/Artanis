namespace Artanis.Configuration;

using System;

public class ConfigurationConstants
{
    public const String GlobalConfiguration = "./config/global.json";
    public const String ShardConfiguration = "./config/shards/${shardid}.json";
    public const String GuildConfiguration = "./config/guilds/${guildid}.json";
}
