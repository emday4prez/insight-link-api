using Microsoft.EntityFrameworkCore;

public class Link
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}


// class represents db session
// query and save data
public class LinkDbContext(DbContextOptions<LinkDbContext> options) : DbContext(options)
{
    public DbSet<Link> Links => Set<Link>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ensures short codes are unique
        modelBuilder.Entity<Link>()
            .HasIndex(l => l.ShortCode)
            .IsUnique();
    }
}