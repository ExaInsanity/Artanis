namespace Artanis.Modlog.Reference;

using System;

public enum ModlogEntryType : Byte
{
    warn,
    mute,
    blacklist,
    kick,
    ban
}