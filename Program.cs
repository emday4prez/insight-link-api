using System.Collections.Concurrent;
using Microsoft.AspNetCore.Routing.Tree;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// in memory db
var links = new ConcurrentDictionary<string, string>();

app.MapPost("/shorten", (LinkRequest request, HttpContext context) =>
{
    if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var inputUri))
    {
        return Results.BadRequest("A valid, absolute URL is required.");
    }

    var shortCode = Guid.NewGuid().ToString("N")[..8];

    links.TryAdd(shortCode, request.Url);

    var resultUrl = $"{context.Request.Scheme}://{context.Request.Host}/{shortCode}";
    return Results.Ok(new LinkResponse(resultUrl));
});


// records for clean, immutable DTOs
public record LinkRequest(string Url);
public record LinkResponse(string ShortenedUrl);