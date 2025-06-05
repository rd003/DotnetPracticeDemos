using System.Text.Json;
using JsonCrud.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/api/people", async () =>
{
    try
    {
        List<Person> people = [];

        string filePath = Path.Join("Db", "People.json");
        if (File.Exists(filePath))
        {
            string jsonContent = await File.ReadAllTextAsync(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive= true
            };
            people = JsonSerializer.Deserialize<List<Person>>(jsonContent,options) ?? [];
        }
        else
        {
            Console.WriteLine("File not exists");
        }
        return Results.Ok(people);
    }
    catch (Exception ex)
    {
        Console.WriteLine("====>"+ex.Message);
        return Results.StatusCode(500);
    }
});

app.Run();
