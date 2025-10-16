using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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

public class LinkDbContextFactory : IDesignTimeDbContextFactory<LinkDbContext>
{
    public LinkDbContext CreateDbContext(string[] args)
    {
        // This builds a temporary configuration object to read user secrets
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<Program>() // Crucially, this loads the user secrets
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<LinkDbContext>();
         var connectionString = configuration.GetConnectionString("Database");

        optionsBuilder.UseSqlServer(connectionString);

        return new LinkDbContext(optionsBuilder.Options);
    }
}

