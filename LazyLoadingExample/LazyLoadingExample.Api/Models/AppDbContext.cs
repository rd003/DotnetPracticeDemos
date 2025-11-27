using Microsoft.EntityFrameworkCore;

namespace LazyLoadingExample.Api.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(o => {
            o.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(30);
        });
        modelBuilder.Entity<Book>(o => {
            o.Property(b => b.Title)
              .IsRequired()
              .HasMaxLength(30);
            o.Property(b => b.Author)
            .IsRequired()
            .HasMaxLength(30);
        });

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Book> Books { get; set; }
}
