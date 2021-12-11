namespace Artanis.Datafixer;

using System;

internal record SortedDatafixerRegistryEntry
{
	public Guid DatafixerGuid { get; init; }
	public ITypelessDatafixer? Datafixer { get; init; }
	public Boolean Breaking { get; init; }
	public UInt32 DatafixerId { get; init; }
}
