using DbFirstDemo.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<PersonDbContext>(o => o.UseSqlServer(connectionString));

var app = builder.Build();

app.MapGet("/people", async (PersonDbContext context) =>
{
    var peoples = await context.People
                              .Select(p => new Person
                              {
                                  PersonId = p.PersonId,
                                  PersonEmail = p.PersonEmail,
                                  PersonName = p.PersonName
                              })
                              .ToListAsync();
    return Results.Ok(peoples);
});

app.Run();

