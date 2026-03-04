using System.Globalization;
using System.Text;
using System.Text.Json;
using LudwigsCMS.Formatter;
using LudwigsCMS.Schema;

namespace LudwigsCMS;

public static class BuildAction
{
    private static JsonSerializerOptions options;

    static BuildAction()
    {
        options = new JsonSerializerOptions()
        {
            IncludeFields = true,
            WriteIndented = true,
            AllowTrailingCommas = true,
        };
    }

    public static int DoBuild(BuildOptions options)
    {
        if (!Directory.Exists(options.OutputDirectory))
        {
            Directory.CreateDirectory(options.OutputDirectory);
        }

        foreach (string file in Directory.EnumerateFiles(options.InputDirectory))
        {
            string name = Path.GetFileName(file);
            string text = File.ReadAllText(file);
            PageCongif congif = JsonSerializer.Deserialize<PageCongif>(text);
            File.WriteAllText(Path.Join(options.OutputDirectory, $"{name}.html"), BuildPage(options, congif));
        }

        string template = JsonSerializer.Serialize(new PageCongif());
        File.WriteAllText(Path.Join(options.OutputDirectory, "template.json"), template);

        return 0;
    }

    public static string BuildPage(BuildOptions options, PageCongif congif)
    {
        congif._Header = GetHtmlContent(options, "header").FormatWith(congif.Header);
        congif._Footer = GetHtmlContent(options, "footer").FormatWith(congif.Footer);
        string ret = GetHtmlContent(options, congif.HtmlSource).FormatWith(congif);

        return ret;
    }

    public static string GetHtmlContent(BuildOptions options, string name)
    {
        return File.ReadAllText(Path.Join(options.ContentDirectory, $"html/{name}.html"));
    }
}