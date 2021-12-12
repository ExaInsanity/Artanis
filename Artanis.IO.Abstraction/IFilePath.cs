namespace Artanis.IO.Abstraction;

using System;

public interface IFilePath
{
    public String Compile();

    public FileType Type { get; }

    public IFilePath SetTargetObject(UInt64 snowflake);

    public IFilePath SetTargetGuild(UInt64 guild);

    public IFilePath SetFileType(FileType file);
}
