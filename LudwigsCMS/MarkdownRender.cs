using Markdig;

namespace LudwigsCMS;

public static class MarkdownRender
{
    public static string md(string mdcontent)
    {
        return Markdown.ToHtml(mdcontent, Program.CustomPipeline);
    }
}