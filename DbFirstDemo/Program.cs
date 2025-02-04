using DbFirstDemo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

string connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<PersonDbContext>(o => o.UseSqlServer(connectionString));

var app = builder.Build();

app.MapGet("/people", async (PersonDbContext context) =>
{
    var peoples = await context.People
                              .ToListAsync();
    return Results.Ok(peoples);
});

app.Run();
