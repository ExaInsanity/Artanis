namespace Artanis.Datafixer;

using System;
using System.Collections.Generic;

internal class DatafixerRegistry
{
    private readonly List<DatafixerRegistryEntry> RawRegistry;
    private readonly Dictionary<Type, List<SortedDatafixerRegistryEntry>> SortedRegistry;
    private Boolean Sorted;

    public DatafixerRegistry()
    {
        this.RawRegistry = new();
        this.SortedRegistry = new();
        this.Sorted = false;
    }

    public IEnumerable<SortedDatafixerRegistryEntry> GetRequiredDatafixers(Type type)
    {
        if(!this.Sorted)
        {
            this.SortRawRegistry();
        }

        if(!this.SortedRegistry.ContainsKey(type))
        {
            return null!;
        }

        return this.SortedRegistry[type];
    }

    public IEnumerable<SortedDatafixerRegistryEntry> GetRequiredDatafixers(String typename)
    {
        if(!this.Sorted)
        {
            this.SortRawRegistry();
        }

        if(!this.SortedRegistry.ContainsKey(Type.GetType(typename)!))
        {
            return null!;
        }

        return this.SortedRegistry[Type.GetType(typename)!];
    }

    public void SortRawRegistry()
    {
        foreach(DatafixerRegistryEntry v in this.RawRegistry)
        {
            if(!this.SortedRegistry.ContainsKey(v.DatafixerTarget!))
            {
                this.SortedRegistry.Add(v.DatafixerTarget!, new());
            }

            this.SortedRegistry[v.DatafixerTarget!].Add(v.ToSortedRegistryEntry());
        }

        this.RawRegistry.Clear();
        this.Sorted = true;
    }

    public void AddRegistryItem(DatafixerRegistryEntry item)
    {
        this.RawRegistry.Add(item);
        this.Sorted = false;
    }

    public void RemoveRegistryItem(DatafixerRegistryEntry item)
    {
        if(this.RawRegistry.Contains(item))
        {
            this.RawRegistry.Remove(item);
        }
        else if(this.SortedRegistry[item.DatafixerTarget!].Contains(item.ToSortedRegistryEntry()))
        {
            this.SortedRegistry[item.DatafixerTarget!].Remove(item.ToSortedRegistryEntry());
        }
    }

    public Dictionary<Type, List<SortedDatafixerRegistryEntry>> GetAllDatafixers()
    {
        if(!this.Sorted)
        {
            this.SortRawRegistry();
        }

        return this.SortedRegistry;
    }
}
