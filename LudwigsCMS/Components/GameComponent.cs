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
                               <a href="{ItchioUrl}" type="button" class="btn">Itch.io</a>
                               """);
        }

        if (SteamUrl != null)
        {
            steamButton = html($"""
                                <a href="{SteamUrl}" type="button" class="btn">Steam</a>
                                """);
        }

        return html($"""
                     <div class="gameThumb" style="background-image: url({ImageThumb})">
                     <span>{Name}</span>
                     <span>{Description}</span>
                     {itchButton}
                     {steamButton}
                     </div>
                     """);
    }
}