using System.Configuration;
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


app.MapPut("/api/people", async (IConfiguration config, Person person) =>
{
    var container = GetContainer(config);

    ItemResponse<Person> response = await container.ReplaceItemAsync(person, person.Id, partitionKey: new PartitionKey(person.Id));

    Console.WriteLine($"====> RU {response.RequestCharge}");

    return Results.Ok(person);
});

app.MapGet("/api/people/{id}", async (IConfiguration config, string id) =>
{
    var container = GetContainer(config);

    ItemResponse<Person> response = await container.ReadItemAsync<Person>(id: id, partitionKey: new PartitionKey(id));
    Console.WriteLine($"====> RU {response.RequestCharge}");

    return Results.Ok(response.Resource);
});

app.MapGet("/api/people", async (IConfiguration config) =>
{
    var container = GetContainer(config);
    var iterator = container.GetItemQueryIterator<Person>();
    var people = new List<Person>();

    while (iterator.HasMoreResults)
    {
        var response = await iterator.ReadNextAsync();
        Console.WriteLine($"====> RU {response.RequestCharge}");
        people.AddRange(response);
    }
    return Results.Ok(people);
});


app.MapDelete("/api/people/{id}", async (IConfiguration config, string id) =>
{
    var container = GetContainer(config);

    ItemResponse<Person> response = await container.DeleteItemAsync<Person>(id: id, partitionKey: new PartitionKey(id));

    Console.WriteLine($"====> RU {response.RequestCharge}");

    return Results.NoContent();
});

app.Run();


static Container GetContainer(IConfiguration config)
{
    string connectionString = config["CosmosDb:ConnectionString"] ?? throw new ConfigurationErrorsException("CosmosDb:ConnectionString");

    var client = new CosmosClient(connectionString);

    string databaseName = config["CosmosDb:Database"] ?? throw new ConfigurationErrorsException("CosmosDb:Database");

    var database = client.GetDatabase(databaseName);

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