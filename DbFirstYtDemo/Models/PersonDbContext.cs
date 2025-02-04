using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DbFirstYtDemo.Models;

public partial class PersonDbContext : DbContext
{
    public PersonDbContext()
    {
    }

    public PersonDbContext(DbContextOptions<PersonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }
    public virtual DbSet<Category> Categories { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseSqlServer("Name=ConnectionStrings:default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK_People_PersonId");

            entity.HasIndex(e => e.PersonEmail, "IX_People_Email").IsUnique();

            entity.Property(e => e.PersonId).HasColumnName("PersonID");
            entity.Property(e => e.ContactNumber).HasMaxLength(20);
            entity.Property(e => e.PersonEmail).HasMaxLength(50);
            entity.Property(e => e.PersonName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
