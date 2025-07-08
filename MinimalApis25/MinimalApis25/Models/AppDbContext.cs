using Microsoft.EntityFrameworkCore;

namespace MinimalApis25.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(mb =>
        {
            mb.Property(p => p.FirstName).IsRequired().HasMaxLength(30);
            mb.Property(p => p.LastName).IsRequired().HasMaxLength(30);
        });
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Person> People { get; set; }
}
