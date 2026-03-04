using System.Reflection;
using CommandLine;
using LudwigsCMS;

public static class Program
{
    public static int Main(string[] args)
    {
        string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        Console.WriteLine($"Running Ludwig's Content Management System v{version}");

        return Parser.Default.ParseArguments<BuildOptions>(args)
            .MapResult(
                BuildAction.DoBuild,
                errors => 1);
    }
}