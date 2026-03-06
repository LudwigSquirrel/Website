namespace LudwigsCMS.Components;

public class GameComponent
{
    public string ItchioUrl { get; set; } = "";
    public string SteamUrl { get; set; } = "";
    public string Name { get; set; } = "Game Name";
    public string Description { get; set; } = "Game Description";
    public string ImageThumb { get; set; } = "ludwig-itch.png";

    public string RenderGame()
    {
        string itchButton = null;
        string steamButton = null;
        if (ItchioUrl != null)
        {
            itchButton = RenderGameStoreLink(ItchioUrl, "Itch.io");
        }

        if (SteamUrl != null)
        {
            steamButton = RenderGameStoreLink(SteamUrl, "Steam");
        }

        return html($"""
                     <div class="gameGalleryItem">
                     <img class="gameThumb" src="{ImageThumb}" alt="{Description}"></img>
                     <h1 class="fw-bold border-bottom mb-3">{Name}</h1>
                     <div class="m-2 fs-6 p-2 gameDescription">{Description}</div>
                     <div class="gameStoreLinkContainer">
                     {itchButton}
                     {steamButton}
                     </div>
                     </div>
                     
                     """);
    }
    
    public static string RenderGameStoreLink(string href, string text)
    {
        return html($"""
                     <a href="{href}" type="button" class="btn m-2 rounded-0 gameStoreLink" role="button">{text}</a>
                     """);
    }
}