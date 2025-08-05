using CodeHollow.FeedReader;
using RssNewsMinimalApi.Models;

namespace RssNewsMinimalApi.Services;

public class RssService
{
    public async Task<List<NewsItem>> GetNewsAsync(string url)
    {
        var feed = await FeedReader.ReadAsync(url);

        return feed.Items.Select(item => new NewsItem
        {
            Title = item.Title,
            Link = item.Link,
            PublishDate = item.PublishingDate?.ToString("yyyy-MM-dd HH:mm") ?? "brak daty"
        }).ToList();
    }
}
