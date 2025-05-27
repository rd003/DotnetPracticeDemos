using MiddleWareDemo;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<ISomeService, SomeService>();
builder.Services.AddTransient<ISomeService, SomeService>();

// builder.Services.AddTransient<FactoryBasedMiddleware>();

var app = builder.Build();

app.Map("/", () =>
{
    return "Hello";
});

app.Map("/greetings", () =>
{
    return "Hello world";
});

app.UseMiddleware<RequestLoggingMiddleWare>();

// app.UseMiddleware<FactoryBasedMiddleware>();

app.Run();
