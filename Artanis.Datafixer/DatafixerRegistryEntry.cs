namespace Artanis.Datafixer;

using System;

internal record DatafixerRegistryEntry
{
    public Guid DatafixerGuid { get; init; }
    public ITypelessDatafixer? Datafixer { get; init; }
    public Type? DatafixerTarget { get; init; }
    public Boolean Breaking { get; init; }
    public UInt32 DatafixerId { get; init; }

    public SortedDatafixerRegistryEntry ToSortedRegistryEntry()
    {
        return new()
        {
            DatafixerGuid = this.DatafixerGuid,
            Datafixer = this.Datafixer,
            Breaking = this.Breaking,
            DatafixerId = this.DatafixerId
        };
    }
}
