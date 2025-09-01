using AzureBlobDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureBlobDemo.Controllers;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Person> People { get; set; }
}