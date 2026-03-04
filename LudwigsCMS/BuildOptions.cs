using CommandLine;

namespace LudwigsCMS;

[Verb("build", HelpText = "Builds the website's content.")]
public class BuildOptions : BaseOptions
{
    [Option('i', "inputDirectory", Default = "input", HelpText = "Specifies the input directory.")]
    public string InputDirectory { get; set; }
    
    [Option('o', "outputDirectory", Default = ".output", HelpText = "Specifies the output directory.")]
    public string OutputDirectory { get; set; }
    
    [Option('c', "contentDirectory", Default = "Content", HelpText = "Specifies the content directory.")]
    public string ContentDirectory { get; set; }
}