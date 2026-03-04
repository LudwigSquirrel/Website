namespace LudwigsCMS;

public static class SiteContent
{
    public static string GetContentPath(string file) => Path.Join(CONTENTPATH, file);
}