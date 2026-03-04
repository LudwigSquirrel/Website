namespace LudwigsCMS.Schema;

public class PageCongif
{
    public string HtmlSource { get; set; }
    public HeaderConfig Header { get; set; } = new();
    public string _Header { get; set; }
    public FooterConfig Footer { get; set; } = new();
    public string _Footer { get; set; }
    public string Body { get; set; }
}