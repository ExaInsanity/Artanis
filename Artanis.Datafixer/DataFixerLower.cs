namespace Artanis.Datafixer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public static class DataFixerLower
{
    private static readonly DatafixerRegistry Registry;

    static DataFixerLower()
    {
        Registry = new();
    }

    public static void SortRegistry()
    {
        Registry.SortRawRegistry();
    }

    public static void AddDatafixer(ITypelessDatafixer datafixer, Type target)
    {
        Registry.AddRegistryItem(new()
        {
            Datafixer = datafixer,
            Breaking = datafixer.Breaking,
            DatafixerGuid = Guid.NewGuid(),
            DatafixerId = datafixer.DatafixerId,
            DatafixerTarget = target
        });
    }

    public static void LoadAllDatafixers()
    {
        Dictionary<Type, List<SortedDatafixerRegistryEntry>> datafixers = Registry.GetAllDatafixers();

        Parallel.ForEach(datafixers, xm =>
        {
            Parallel.ForEach(xm.Value, xn =>
            {
                xn.GetType().InvokeMember("Load", BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod,
                    null, null, null);
            });
        });
    }

    public static Datafixable UpgradeData<Datafixable>(Datafixable data)
        where Datafixable : IDatafixable
    {
        Datafixable reference = data;
        List<SortedDatafixerRegistryEntry> datafixers;

        try
        {
            datafixers = Registry.GetRequiredDatafixers(data.GetType()).ToList();
        }
        catch(ArgumentNullException)
        {
            return reference;
        }

        if(datafixers == null)
        {
            return reference;
        }

        foreach(SortedDatafixerRegistryEntry v in datafixers)
        {
            ((IDatafixer<Datafixable>)v.Datafixer!).UpgradeData(ref reference);
        }

        return reference;
    }

    public static Datafixable DowngradeData<Datafixable>(Datafixable data)
        where Datafixable : IDatafixable
    {
        Datafixable reference = data;
        List<SortedDatafixerRegistryEntry> datafixers;

        try
        {
            datafixers = Registry.GetRequiredDatafixers(data.GetType()).ToList();
        }
        catch(ArgumentNullException)
        {
            return reference;
        }

        if(datafixers == null)
        {
            return reference;
        }

        foreach(SortedDatafixerRegistryEntry v in datafixers)
        {
            ((IDatafixer<Datafixable>)v.Datafixer!).DowngradeData(ref reference);
        }

        return reference;
    }
}
