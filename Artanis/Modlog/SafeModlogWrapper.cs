namespace Artanis.Modlog;

using System;
using System.Threading.Tasks;

using Artanis.Modlog.Reference;

using Remora.Discord.API.Abstractions.Objects;

public static class SafeModlogWrapper
{
    /// <summary>
    /// Attempts to add a modlog entry to a specific user. Returns true if successful.
    /// </summary>
    public static Task<Boolean> TryAddModlogEntry(this IGuildMember user, ModlogEntry modlogEntry)
    {
        if(user == null)
        {
            throw new ArgumentException("Could not add modlog entry to nonexistent user", nameof(user));
        }

        try
        {
            user.AddModlogEntry(modlogEntry);
            return Task.FromResult(true);
        }
        catch(Exception e)
        {
            Console.WriteLine($"{e}: {e.Message}\n{e.StackTrace}");
            return Task.FromResult(false);
        }
    }

    /// <summary>
    /// Attempts to add a modlog entry to a specific user, time being inferred. Returns true if successful.
    /// </summary>
    public static Task<Boolean> TryAddModlogEntry(this IGuildMember user, ModlogEntryType type, String reason)
    {
        if(user == null)
        {
            throw new ArgumentException("Could not add modlog entry to nonexistent user", nameof(user));
        }

        try
        {
            user.AddModlogEntry(type, reason);
            return Task.FromResult(true);
        }
        catch(Exception e)
        {
            Console.WriteLine($"{e}: {e.Message}\n{e.StackTrace}");
            return Task.FromResult(false);
        }
    }

    /// <summary>
    /// Attempts to add a verballog entry to a specific user. Returns true if successful.
    /// </summary>
    public static Task<Boolean> TryAddVerballogEntry(this IGuildMember user, VerbalModlogEntry entry)
    {
        if(user == null)
        {
            throw new ArgumentException("Could not add modlog entry to nonexistent user", nameof(user));
        }

        try
        {
            user.AddVerbalModlogEntry(entry);
            return Task.FromResult(true);
        }
        catch(Exception e)
        {
            Console.WriteLine($"{e}: {e.Message}\n{e.StackTrace}");
            return Task.FromResult(false);
        }
    }

    /// <summary>
    /// Attempts to add a verballog entry to a specific user, time being inferred. Returns true if successful.
    /// </summary>
    public static Task<Boolean> TryAddVerballogEntry(this IGuildMember user, String reason)
    {
        if(user == null)
        {
            throw new ArgumentException("Could not add modlog entry to nonexistent user", nameof(user));
        }

        try
        {
            user.AddVerbalModlogEntry(reason);
            return Task.FromResult(true);
        }
        catch(Exception e)
        {
            Console.WriteLine($"{e}: {e.Message}\n{e.StackTrace}");
            return Task.FromResult(false);
        }
    }

    /// <summary>
    /// Attempts to fetch a specific user's modlog. Returns true if successful.
    /// </summary>
    /// <param name="modlog">The variable the user's modlog will be assigned to.</param>
    public static Task<Boolean> TryFetchModlog(this IGuildMember user, out UserModlog modlog)
    {
        if(user == null)
        {
            throw new ArgumentException("Could not fetch modlog of nonexistent user", nameof(user));
        }

        try
        {
            modlog = user.GetUserModlog();
            return Task.FromResult(true);
        }
        catch(Exception e)
        {
            Console.WriteLine($"{e}: {e.Message}\n{e.StackTrace}");
            modlog = null!;
            return Task.FromResult(false);
        }
    }

    /// <summary>
    /// Attempts to set a specific user's modlog. Returns true if successful.
    /// </summary>
    public static Task<Boolean> TrySetModlog(this IGuildMember user, UserModlog modlog)
    {
        if(user == null)
        {
            throw new ArgumentException("Could not set modlog of nonexistent user", nameof(user));
        }

        try
        {
            user.SetUserModlog(modlog);
            return Task.FromResult(true);
        }
        catch(Exception e)
        {
            Console.WriteLine($"{e}: {e.Message}\n{e.StackTrace}");
            return Task.FromResult(false);
        }
    }
}