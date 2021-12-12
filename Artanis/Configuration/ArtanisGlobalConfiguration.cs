namespace Artanis.Configuration;

using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

internal class ArtanisGlobalConfiguration
{
    public String Token { get; set; } = null!;

    public JsonDocument Document { get; set; } = null!;

    public async Task Load()
    {
        JsonDocument doc = null!;

        try
        {
            StreamReader reader = new(ConfigurationConstants.GlobalConfiguration);
            doc = await JsonDocument.ParseAsync(reader.BaseStream);
            reader.Close();
        }
        catch(FileNotFoundException)
        {
            // TODO: fetch config from github
        }

        if(doc != null)
        {
            this.Token = doc.SelectElement("token")!.Value.GetString()!;
            this.Document = doc;
        }
    }

    public T Value<T>(String path)
    {
        return (T)this.Document.SelectElement(path)!.Value.GetObjectValue();
    }
}
