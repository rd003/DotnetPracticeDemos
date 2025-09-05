using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/api/people", async (IConfiguration config, Person person) =>
{
    var container = GetContainer(config);

    ItemResponse<Person> response = await container.CreateItemAsync(item: person, partitionKey: new PartitionKey(person.Id));

    Console.WriteLine($"====> RU {response.RequestCharge}");

    return Results.Ok(person);
});

app.Run();


static Container GetContainer(IConfiguration config)
{
    string connectionString = config.GetSection("CosmosDb:ConnectionString").Value ?? throw new InvalidOperationException("No connection string found");

    var client = new CosmosClient(connectionString);

    var database = client.GetDatabase("CosmosDb:Database");

    var contaier = database.GetContainer("people");

    return contaier;
}

public class Person
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}