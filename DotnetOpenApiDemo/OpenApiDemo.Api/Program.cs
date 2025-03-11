using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // app.UseSwaggerUI(options =>
    // {
    //     options.SwaggerEndpoint("/openapi/v1.json", "v1");
    // });
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapGet("/api/people", () =>
{
    return Results.Ok("People list");
});

app.MapPost("/api/people", () =>
{
    return Results.Ok("Resource is created");
});

app.MapPut("/api/people", () =>
{
    return Results.Ok("Resource is updated");
});

app.MapDelete("/api/people/{id}", (int id) =>
{
    return Results.Ok($"Resource with id:{id} is deleted");
});

app.Run();
