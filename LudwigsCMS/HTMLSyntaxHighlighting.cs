using System.Diagnostics.CodeAnalysis;

namespace LudwigsCMS;

public static class HTMLSyntaxHighlighting
{
    public static string html([StringSyntax("HTML")] string html) => html;
}