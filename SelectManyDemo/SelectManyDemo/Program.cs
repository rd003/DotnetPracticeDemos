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


// get department's all employee

app.MapGet("/api/departments/{departmentId}/employees", async (int departmentId, AppDbContext context) =>
{
    var employees = await context.Departments
                           .Where(d => d.DepartmentId == departmentId)
                           .SelectMany(d => d.Employees)
                           .Select(e => e.Name)
                           .ToListAsync();
    return Results.Ok(employees);
});

// get employee's skills

app.MapGet("api/employees/{employeeId}/skills", async (int employeeId, AppDbContext context) =>
{
    var skills = await context.Employees
                        .Where(e => e.EmployeeId == employeeId)
                        .SelectMany(e => e.EmployeeSkills)
                        .Select(es => es.Skill.Name)
                        .ToListAsync();
    return Results.Ok(skills);
});


app.Run();

public record EmployeeWithSkill(int EmployeeId, string Name, string DepartmentName, string[] Skills);