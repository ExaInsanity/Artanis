namespace Artanis.IO.File;

using System.IO;
using System.Text.Json;

using Artanis.IO.Abstraction;

public class FileSystemActor : IFileSystemActor
{
    public JsonDocument LoadData(IFilePath path)
    {
        return JsonDocument.Parse(File.ReadAllText(path.Compile()));
    }

    public void WriteData(JsonDocument document, IFilePath path)
    {
        using StreamWriter stream = new(path.Compile());
        using Utf8JsonWriter writer = new(stream.BaseStream);
        document.WriteTo(writer);
        stream.Flush();
    }
}
