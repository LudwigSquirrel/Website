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
            itchButton = html($"""
                               <a href="{ItchioUrl}" type="button" class="btn m-2 btn-primary gameStoreLink" role="button">Itch.io</a>
                               """);
        }

        if (SteamUrl != null)
        {
            steamButton = html($"""
                                <a href="{SteamUrl}" type="button" class="btn m-2 btn-primary gameStoreLink" role="button">Steam</a>
                                """);
        }

        return html($"""
                     <div class="gameGalleryItem">
                     <span class="fs-1">{Name}</span><br>
                     <div class="gameThumb p-2" style="background-image: url({ImageThumb})"></div>
                     <span class="fs-6">{Description}</span>
                     <div class="gameStoreLinkContainer">
                     {itchButton}
                     {steamButton}
                     </div>
                     </div>
                     
                     """);
    }
}