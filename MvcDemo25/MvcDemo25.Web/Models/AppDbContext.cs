using Microsoft.EntityFrameworkCore;

namespace MvcDemo25.Web.Models;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
	{

	}
    public DbSet<Person> People { get; set; }
}
