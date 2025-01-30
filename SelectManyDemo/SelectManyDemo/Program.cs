using Microsoft.EntityFrameworkCore;
using SelectManyDemo.ExtensionMethods;
using SelectManyDemo.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("InMem"));

var app = builder.Build();
app.UseHttpsRedirection();
app.InitializeDb();

app.MapGet("/api/employees", async (AppDbContext context) =>
{
    var employees = await context.Employees
                        .Include(e => e.Department)
                        .Select(e =>
                          new EmployeeWithSkill(
                              e.EmployeeId,
                              e.Name,
                              e.Department.Name,
                              e.EmployeeSkills
                               .Select(es => es.Skill.Name)
                               .ToArray()
                         ))
                        .ToListAsync();
    return Results.Ok(employees);
});


app.Run();

public record EmployeeWithSkill(int EmployeeId, string Name, string DepartmentName, string[] Skills);