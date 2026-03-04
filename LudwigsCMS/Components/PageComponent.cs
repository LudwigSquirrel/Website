using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Markdig;

namespace LudwigsCMS.Components;

public enum PageType
{
    Normal,
    GamesGallery,
}

public class PageComponent
{
    public HeaderComponent Header { get; set; } = new();
    public string MarkdownBodySource { get; set; } = "";

    [JsonIgnore]
    public string HtmlOutputName => $"{MarkdownBodySource}.html";

    public GameComponent[] Games { get; set; } = [];

    public PageType Type { get; set; } = PageType.Normal;

    private static string Scripts => html("""
                                          <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
                                          <script src="enable-tooltips.js" crossorigin="anonymous"></script>
                                          """);

    public string Render(Navigation navigation)
    {
        string source = SiteContent.GetContentPath($"markdown/{MarkdownBodySource}.md");
        string md = File.Exists(source) ? File.ReadAllText(source) : "# Oh No!\nNo content :(";
        switch (Type)
        {
            case PageType.Normal:
                return html($"""
                             <!DOCTYPE html>
                             <html lang="en">
                             {Header.Render()}
                             <body>
                             {navigation.Render(this)}
                             <div class="container">
                             {Markdown.ToHtml(md, Program.CustomPipeline)}
                             </div>
                             {Scripts}
                             </body>
                             </html>
                             """);
            case PageType.GamesGallery:
                return html($"""
                             <!DOCTYPE html>
                             <html lang="en">
                             {Header.Render()}
                             <body>
                             {navigation.Render(this)}
                             <div class="container">
                             {Markdown.ToHtml(md, Program.CustomPipeline)}
                             {string.Join('\n', Games.Select(RenderGame))}
                             </div>
                             {Scripts}
                             </body>
                             </html>
                             """);
        }

        return "";
    }

    public string RenderGame(GameComponent game)
    {
        return html($"""
                     <div>
                     <div class="gameThumb"></div><br>
                     <span>{game.Name}</span><br>
                     <span>{game.Description}</span>
                     {(game.ItchioUrl != null ? $"<a href=\"{game.ItchioUrl}\"></a>" : "")}
                     {(game.SteamUrl != null ? $"<a href=\"{game.SteamUrl}\"></a>" : "")}
                     </div>
                     """);
    }
}