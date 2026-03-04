using CommandLine;

namespace LudwigsCMS;

public class Options
{
    [Option(Default = false)]
    public bool HotReload { get; set; }
}