using System.Text.Json;
using System.Text.Json.Serialization;
using LudwigsCMS.Components;

namespace LudwigsCMS;

public class SiteConfig
{
    private static JsonSerializerOptions options;


    static SiteConfig()
    {
        options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            AllowTrailingCommas = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };
    }

    public PageComponent[] Pages { get; set; }= [new()];

    public static SiteConfig Load()
    {
        try
        {
            string text = File.ReadAllText(SiteContent.GetContentPath("config.json"));
            return JsonSerializer.Deserialize<SiteConfig>(text, options);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new SiteConfig();
        }
    }

    public static void Save(SiteConfig config)
    {
        File.WriteAllText("output/config.json", JsonSerializer.Serialize(config, options));
    }
}