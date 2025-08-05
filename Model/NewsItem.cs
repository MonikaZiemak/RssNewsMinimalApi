namespace RssNewsMinimalApi.Model;

public class NewsItem
{
    public string Title { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? Link { get; set; }
    public DateTime PublishDate { get; set; }
}
