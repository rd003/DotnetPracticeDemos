using CartesianExplosionExample.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(o=>o.UseSqlite("Data source = person_db.db"));
var app = builder.Build();

app.MapGet("/", async (AppDbContext ctx) => {
    var people = await ctx.People
       .Include(p => p.Emails)
       .Include(p => p.Addresses)
       .ToListAsync();
    return Results.Ok(people);
});

app.MapGet("/split", async (AppDbContext ctx) => {
    var people = await ctx.People
       .Include(p => p.Emails)
       .Include(p => p.Addresses)
       .AsSplitQuery()
       .ToListAsync();
    return Results.Ok(people);
});

app.Run("http://localhost:5000");