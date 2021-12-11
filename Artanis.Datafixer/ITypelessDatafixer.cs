namespace Artanis.Datafixer;

using System;

public interface ITypelessDatafixer
{
	public void Load()
    {
		// empty. by default nothing happens here.
    }

	public Version NewDataVersion { get; }
	public Version OldDataVersion { get; }
	public UInt32 DatafixerId { get; }
	public Boolean Breaking { get; }
}
