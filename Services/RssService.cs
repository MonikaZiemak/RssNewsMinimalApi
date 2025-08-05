using CodeHollow.FeedReader;
using RssNewsMinimalApi.Model;

namespace RssNewsMinimalApi.Services;

public class RssService
{
    public async Task<List<NewsItem>> GetNewsAsync(string url)
    {
        var newsItems = feed.Items.Select(item => new NewsItem
        {
            Title = item.Title.Text,
            Summary = item.Summary?.Text,
            Link = item.Links.FirstOrDefault()?.Uri.ToString(),
            PublishDate = item.PublishDate.UtcDateTime // lub .DateTime
        }).ToList();
    }
}
