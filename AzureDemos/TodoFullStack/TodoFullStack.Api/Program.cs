using Microsoft.EntityFrameworkCore;
using TodoFullStack.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(op=>op.UseInMemoryDatabase("InMem"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
if (!dbContext.Todos.Any())
{
    dbContext.Todos.AddRange([
        new(){Title="Cosmos Db", Completed=true},
        new() {Title="Azure static web apps"}
    ]);
    dbContext.SaveChanges();
}


app.Run();
