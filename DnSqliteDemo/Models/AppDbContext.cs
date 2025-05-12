using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DnSqliteDemo.Models;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
    
  }

  public DbSet<Person> People { get; set; }
}


public class Person
{
    public int Id { get; set; }
    
    [MaxLength(20)]
    public string FirstName { get; set; } = null!;
    [MaxLength]
    public string LastName { get; set; } = null!;
}