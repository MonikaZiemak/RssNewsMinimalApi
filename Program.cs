using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja Swaggera
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minimal RSS API",
        Version = "v1",
        Description = "Pobiera najnowsze wiadomoœci z kana³u RSS (Google News)"
    });
});

var app = builder.Build();

// Middleware Swaggera
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal RSS API v1");
});

// Endpoint: GET /news
app.MapGet("/news", async () =>
{
    var feedUrl = "https://news.google.com/rss?hl=pl&gl=PL&ceid=PL:pl";

    using var reader = XmlReader.Create(feedUrl);
    var feed = SyndicationFeed.Load(reader);

    var newsItems = feed.Items.Select(item => new
    {
        Title = item.Title.Text,
        Summary = item.Summary?.Text,
        Link = item.Links.FirstOrDefault()?.Uri.ToString(),
        PublishDate = item.PublishDate.DateTime
    });

    return Results.Ok(newsItems);
})
.WithName("GetNews")
.WithOpenApi();

// Przekierowanie z "/" na "/swagger"
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
