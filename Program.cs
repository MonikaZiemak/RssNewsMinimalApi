using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.OpenApi.Models;
using RssNewsMinimalApi.Model; // Upewnij siê, ¿e ten namespace jest poprawny

var builder = WebApplication.CreateBuilder(args);

// Dodaj Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minimal RSS API",
        Version = "v1",
        Description = "Pobiera najnowsze wiadomoœci z kana³u Google News RSS"
    });
});

var app = builder.Build();

// Middleware Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal RSS API v1");
    options.RoutePrefix = ""; // dziêki temu Swagger dzia³a na stronie g³ównej
});

// Endpoint: GET /news
app.MapGet("/news", () =>
{
    var feedUrl = "https://news.google.com/rss?hl=pl&gl=PL&ceid=PL:pl";

    using var reader = XmlReader.Create(feedUrl);
    var feed = SyndicationFeed.Load(reader);

    var newsItems = feed.Items.Select(item => new NewsItem
    {
        Title = item.Title.Text,
        Summary = item.Summary?.Text,
        Link = item.Links.FirstOrDefault()?.Uri.ToString(),
        PublishDate = item.PublishDate.UtcDateTime
    }).ToList();

    return Results.Ok(newsItems);
})
.WithName("GetNews")
.WithOpenApi();

app.Run();
