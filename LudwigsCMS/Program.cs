using System.Reflection;
using CommandLine;
using LudwigsCMS;
using Markdig;

public static class Program
{
    public static readonly MarkdownPipeline CustomPipeline;

    static ManualResetEvent quitEvent = new(false);

    static Program()
    {
        CustomPipeline = new MarkdownPipelineBuilder()
            .UseBootstrap()
            .UseGenericAttributes()
            .Build();
    }

    public static int Main(string[] args)
    {
        Console.CancelKeyPress += (sender, eventArgs) =>
        {
            quitEvent.Set();
            eventArgs.Cancel = true;
        };

        string version = Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString() ?? "Unkown";
        Console.WriteLine($"Running Ludwig's Content Management System v{version}");

        Options options = Parser.Default.ParseArguments<Options>(args).Value;

        FileSystemWatcher watcher = null!;
        if (options.HotReload)
        {
            watcher = new FileSystemWatcher(CONTENTPATH);
            watcher.NotifyFilter = NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName
                                   | NotifyFilters.DirectoryName;
            watcher.Changed += (sender, eventArgs) =>
            {
                if (eventArgs.ChangeType == WatcherChangeTypes.Changed)
                {
                    if (SiteBuild.Building == false)
                    {
                        Console.WriteLine($"Change detected in {eventArgs.Name}. Building...");
                        SiteBuild.BuildWebsite();
                        Console.WriteLine("Done!");
                    }
                }
            };
            watcher.EnableRaisingEvents = true;
        }

        Console.WriteLine("Building...");
        SiteBuild.BuildWebsite();
        Console.WriteLine("Done!");

        if (options.HotReload)
        {
            Console.WriteLine("Hot Reload Enabled.");
            quitEvent.WaitOne();
            watcher?.Dispose();
        }

        Console.WriteLine("Finished!");

        return 0;
    }
}