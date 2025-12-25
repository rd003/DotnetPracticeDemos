using Microsoft.EntityFrameworkCore;

namespace CartesianExplosionExample.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Person> People { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Email> Emails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasData(
            new Person { Id = 1, FirstName = "Ramesh", LastName = "Singh" },
            new Person { Id = 2, FirstName = "Mahesh", LastName = "Kumar" }
            );

        modelBuilder.Entity<Email>().HasData(
            new Email { Id = 1, PersonId = 1, PersonEmail = "ramesh.singh@example.com" },
            new Email { Id = 2, PersonId = 1, PersonEmail = "singh.ramesh12@example.com" },
        new Email { Id = 3, PersonId = 1, PersonEmail = "singh.ramesh13@work.com" },
        new Email { Id = 4, PersonId = 2, PersonEmail = "mahesh.kumar@example.com" },
        new Email { Id = 5, PersonId = 2, PersonEmail = "mahesh.kumar12@work.com" });

        modelBuilder.Entity<Address>().HasData(
            new Address {Id=1, PersonId = 1, PersonAddress = "somewhere in Mumbai"},
            new Address {Id=2, PersonId = 1, PersonAddress = "some place in Mumbai"},
            new Address {Id=3, PersonId = 1, PersonAddress = "somewhere in Hyderabad"},
            new Address {Id=4, PersonId = 2, PersonAddress = "somewhere in Delhi"},
            new Address {Id=5, PersonId = 2, PersonAddress = "somewhere in Haryana" }
            );

        base.OnModelCreating(modelBuilder);
    }
}
