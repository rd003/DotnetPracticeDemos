using DnSqliteDemo.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o=>o.UseSqlite("Data Source = person.db"));

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
