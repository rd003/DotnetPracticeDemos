using JsonCrud.Person;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPersonEndpoints();

app.Run();

