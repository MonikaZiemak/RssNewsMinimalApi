using CodeHollow.FeedReader;
using RssNewsMinimalApi.Model;

namespace RssNewsMinimalApi.Services;

public class RssService
{
    public async Task<List<NewsItem>> GetNewsAsync(string url)
    {
        try
        {
            var feed = await FeedReader.ReadAsync(url);

            var newsItems = feed.Items.Select(item => new NewsItem
            {
                Title = item.Title,
                Summary = item.Description,
                Link = item.Link,
                PublishDate = item.PublishingDate ?? DateTime.MinValue
            }).ToList();

            return newsItems;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas pobierania feeda: {ex.Message}");
            return new List<NewsItem>();
        }
    }
}
