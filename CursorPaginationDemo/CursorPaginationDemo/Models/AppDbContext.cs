using Microsoft.EntityFrameworkCore;

namespace CursorPaginationDemo.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Book_Id");

            entity.ToTable("Book");

            entity.HasIndex(e => e.Title, "IX_Book_Title_Inc_Id_Author_Price");

            entity.Property(e => e.Author).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.ImageLink).HasMaxLength(100);
            entity.Property(e => e.Language).HasMaxLength(20);
            entity.Property(e => e.Link).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}