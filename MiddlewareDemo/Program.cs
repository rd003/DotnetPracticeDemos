using MiddleWareDemo;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<ISomeService, SomeService>();
//builder.Services.AddTransient<ISomeService, SomeService>();

// builder.Services.AddTransient<FactoryBasedMiddleware>();

var app = builder.Build();

app.MapGet("/greetings", () =>
{
    return "Hello world";
});


app.Use(async (context, next) =>
{
    // adding a request header
    context.Request.Headers.Append("X-Custom-Req", "Custom Req");

    // adding a response header
    context.Response.Headers.Append("X-Custom-Response", "Custom Response");

    // Request Headers
    Console.WriteLine("\nREQUEST HEADERS:");
    foreach (var header in context.Request.Headers.OrderBy(h => h.Key))
    {
        Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value.ToArray())}");
    }

    context.Response.ContentType = "text/plain";

    await next();

    // Response Headers
    Console.WriteLine("\nRESPONSE HEADERS:");
    foreach (var header in context.Response.Headers.OrderBy(h => h.Key))
    {
        Console.WriteLine($"  {header.Key}: {string.Join(", ", header.Value.ToArray())}");
    }
    Console.WriteLine("=" + new string('=', 50));

    await context.Response.WriteAsync("\nAppended in middleware 1...\n");
});

app.Use(async (context, next) =>
{
    await next();

    await context.Response.WriteAsync("\nAppended in middleware 2");
});

// app.Use(async (context, next) =>
// {
//     Console.WriteLine("===> Middleware 1 before next() (during req)");
//     await next();
//     Console.WriteLine("===> Middleware 1 after next() (during response)");
// });

// app.Use(async (context, next) =>
// {
//     Console.WriteLine("===> Middleware 2 before next() (during req)");
//     await next();
//     Console.WriteLine("===> Middleware 2 after next() (during response)");
// });

// app.UseMiddleware<RequestLoggingMiddleWare>();

// app.UseMiddleware<FactoryBasedMiddleware>();

app.Run();
