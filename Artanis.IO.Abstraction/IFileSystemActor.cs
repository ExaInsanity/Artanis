namespace Artanis.IO.Abstraction;

using System.Text.Json;

public interface IFileSystemActor
{
    public JsonDocument LoadData(IFilePath path);

    public void WriteData(JsonDocument document, IFilePath path);
}
