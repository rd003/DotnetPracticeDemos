using Microsoft.EntityFrameworkCore;

namespace SelectManyDemo.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     base.OnConfiguring(optionsBuilder);
    //     optionsBuilder.UseInMemoryDatabase("InMem");
    // }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<EmployeeSkill>()
       .HasKey(e => new { e.EmployeeId, e.SkillId });
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<EmployeeSkill> EmployeeSkills { get; set; }
}