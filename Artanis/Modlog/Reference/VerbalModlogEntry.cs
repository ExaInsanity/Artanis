namespace Artanis.Modlog.Reference;

using System;

public struct VerbalModlogEntry
{
    public DateTimeOffset Time { get; set; }

    public String Reason { get; set; }

    public override Boolean Equals(Object? obj)
    {
        if((obj as VerbalModlogEntry?)?.Time == this.Time)
        {
            return true;
        }

        return false;
    }

    public override Int32 GetHashCode() => base.GetHashCode();

    public static Boolean operator ==(VerbalModlogEntry left, VerbalModlogEntry right) => left.Equals(right);

    public static Boolean operator !=(VerbalModlogEntry left, VerbalModlogEntry right) => !(left == right);
}