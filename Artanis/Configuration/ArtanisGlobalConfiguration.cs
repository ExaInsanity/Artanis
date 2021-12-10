namespace Artanis.Configuration;

using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

internal class ArtanisGlobalConfiguration
{
    public String Token { get; set; } = null!;

    public JsonDocument Configuration { get; set; } = null!;

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

        }

        if(doc != null)
        {
            Token = doc.SelectElement("token")!.Value.GetString()!;
            Configuration = doc;
        }

    }
}
