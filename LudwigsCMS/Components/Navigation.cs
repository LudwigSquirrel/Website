namespace LudwigsCMS.Components;

public class Navigation
{
    public PageComponent[] Pages;

    public string Render(PageComponent current)
    {
        return html($"""
                     <nav class="navbar navbar-expand-lg bg-body-tertiary">
                       <div class="container-fluid">
                         <a class="navbar-brand" href="index.html">KSF</a>
                         <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                           <span class="navbar-toggler-icon"></span>
                         </button>
                         <div class="collapse navbar-collapse" id="navbarSupportedContent">
                           <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                             {string.Join('\n', Pages.Select(component => RenderLink(component, current)))}
                           </ul>
                         </div>
                       </div>
                     </nav>
                     """);
    }

    public string RenderLink(PageComponent page, PageComponent current)
    {
      string[] split = page.Header.Title.Split('-');
      string title = split[0];
      string tooltip = split.Length > 0 ? split[1] : "";
        return html($$"""
                      <a  class=""></a>
                      <li class="nav-item" data-bs-toggle="tooltip" data-bs-placement="bottom" data-bs-title="{{tooltip}}">
                        <a class="nav-link {{((page == current) ? "active" : "")}}" aria-current="page" href="{{page.HtmlOutputName}}">{{title}}</a>
                      </li>
                      """);
    }
}