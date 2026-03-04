using LudwigsCMS.Components;

namespace LudwigsCMS;

public static class SiteBuild
{
    public static bool Building { get; private set; }
    public static void BuildWebsite()
    {
        if (Building) return;
        Building = true;
        SiteConfig config = SiteConfig.Load();

        if (!Directory.Exists(OUTPUTPATH))
        {
            Directory.CreateDirectory(OUTPUTPATH);
        }

        Navigation navigation = new Navigation();
        navigation.Pages = config.Pages;

        foreach (PageComponent page in config.Pages)
        {
            try
            {
                SiteBuild.Build(page, navigation, Path.Join(OUTPUTPATH, page.HtmlOutputName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        // Copy style sheet.
        File.Copy(
            Path.Join(CONTENTPATH, "style.css"),
            Path.Join(OUTPUTPATH, "style.css"),
            true);

        // Copy images.
        foreach (var file in Directory.EnumerateFiles(Path.Join(CONTENTPATH, "img")))
        {
            var fileName = Path.GetFileName(file);
            File.Copy(
                Path.Join(CONTENTPATH, $"img/{fileName}"),
                Path.Join(OUTPUTPATH, fileName),
                true);
        }

        // Copy scripts.
        foreach (var file in Directory.EnumerateFiles(Path.Join(CONTENTPATH, "js")))
        {
            var fileName = Path.GetFileName(file);
            File.Copy(
                Path.Join(CONTENTPATH, $"js/{fileName}"),
                Path.Join(OUTPUTPATH, fileName),
                true);
        }

        SiteConfig.Save(config);
        Building = false;
    }

    public static void Build(PageComponent page, Navigation navigation, string file)
    {
        File.WriteAllText(file, page.Render(navigation));
    }
}