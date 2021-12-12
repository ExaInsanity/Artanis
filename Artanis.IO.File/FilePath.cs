namespace Artanis.IO.File;

using System;

using Artanis.IO.Abstraction;

public class FilePath : IFilePath
{
    public FileType Type { get; set; }
    
    private UInt64 Guild { get; set; }
    private UInt64 Target { get; set; }

    public String Compile()
    {
        return this.Type switch
        {
            FileType.DefaultPermission => $"./data/guilds/{this.Guild}/permissions/default.json",
            FileType.RolePermission => $"./data/guilds/{this.Guild}/permissions/roles/{this.Target}.json",
            FileType.GlobalModlog => $"./data/artanis-global/modlogs/{this.Target}.json",
            FileType.Modlog => $"./data/users/{this.Target}/guilds/{this.Guild}/modlog.json",
            FileType.UserPermission => $"./data/users/{this.Target}/guilds/{this.Guild}/permissions.json",
            _ => throw new InvalidProgramException("Invalid enum type")
        };
    }

    public IFilePath SetFileType(FileType file)
    {
        this.Type = file;
        return this;
    }

    public IFilePath SetTargetGuild(UInt64 guild)
    {
        this.Guild = guild;
        return this;
    }

    public IFilePath SetTargetObject(UInt64 snowflake)
    {
        this.Target = snowflake;
        return this;
    }
}
