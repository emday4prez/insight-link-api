using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// --- Configuration ---
var connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<LinkDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// --- API Endpoints ---

// POST /shorten
app.MapPost("/shorten", async (LinkRequest request, HttpContext context, LinkDbContext db) =>
{
    if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var inputUri))
    {
        return Results.BadRequest("A valid, absolute URL is required.");
    }

    var shortCode = Guid.NewGuid().ToString("N")[..8];

    var newLink = new Link
    {
        OriginalUrl = request.Url,
        ShortCode = shortCode,
        CreatedAt = DateTime.UtcNow
    };

    await db.Links.AddAsync(newLink);
    await db.SaveChangesAsync();

    var resultUrl = $"{context.Request.Scheme}://{context.Request.Host}/{shortCode}";
    return Results.Ok(new LinkResponse(resultUrl));
});


// GET /{shortCode}
app.MapGet("/{shortCode}", async (string shortCode, LinkDbContext db) =>
{
    var link = await db.Links.FirstOrDefaultAsync(l => l.ShortCode == shortCode);

    if (link is null)
    {
        return Results.NotFound();
    }

    return Results.Redirect(link.OriginalUrl, permanent: true);
});


app.Run();


// Defines our DTOs
public record LinkRequest(string Url);
public record LinkResponse(string ShortenedUrl);