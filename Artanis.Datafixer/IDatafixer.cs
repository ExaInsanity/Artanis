namespace Artanis.Datafixer;

public interface IDatafixer<Datafixable> : ITypelessDatafixer
    where Datafixable : IDatafixable
{
    public void UpgradeData(ref Datafixable data);
    public void DowngradeData(ref Datafixable data);
}
