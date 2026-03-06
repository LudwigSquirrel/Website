using LudwigsCMS;

[assembly: System.Reflection.Metadata.MetadataUpdateHandler(typeof(HotReloadHandler))]

internal static class HotReloadHandler
{
    public static void UpdateApplication(Type[]? updatedTypes)
    {
        if (!SiteBuild.Building)
        {
            Console.WriteLine("Rebuilding due to hot reload.");
            SiteBuild.BuildWebsite();
            Console.WriteLine("Done!");
        }
    }
}