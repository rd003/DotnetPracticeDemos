using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MinimalApis25.Endpoints;
using MinimalApis25.Models;
using MinimalApis25.Validators;

var builder = WebApplication.CreateBuilder(args);

// register AppDbContext in the DI Container
// Lifetime : scoped
string connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string with key 'Default' is null");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
builder.Services.AddScoped<IValidator<Person>, PersonValidator>();

var app = builder.Build();

app.MapPersonEndpoints();

app.Run();
