using Azure;
using Azure.Data.Tables;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/people", async (PersonDto personDto) =>
{
    if (string.IsNullOrEmpty(personDto.FirstName) || string.IsNullOrEmpty(personDto.LastName))
    {
        return Results.BadRequest("Must provide first and lastname");
    }

    var person = new Person
    {
        FirstName= personDto.FirstName,
        LastName= personDto.LastName,
        PartitionKey = "people"
    };

    TableClient tableClient = await GetTableClientAsync("People");

    Response response = await tableClient.UpsertEntityAsync<Person>(entity:person,mode:TableUpdateMode.Replace);

    return Results.Ok(person);
});

app.MapPut("/people", async (PersonUpdateDto personDto) =>
{
    if (string.IsNullOrEmpty(personDto.RowKey) || string.IsNullOrEmpty(personDto.FirstName) || string.IsNullOrEmpty(personDto.LastName))
    {
        return Results.BadRequest("Must provide first and lastname");
    }

    var person = new Person
    {
        RowKey = personDto.RowKey,
        FirstName = personDto.FirstName,
        LastName = personDto.LastName,
        PartitionKey = "people"
    };

    TableClient tableClient = await GetTableClientAsync("People");

    Response response = await tableClient.UpsertEntityAsync(entity: person, mode: TableUpdateMode.Replace);

    return Results.Ok(person);
});

app.MapGet("/people", async () =>
{
    // Retrieve all rows based on partition id

    TableClient tableClient = await GetTableClientAsync("People");

    var pageable = tableClient.QueryAsync<Person>(filter: "PartitionKey eq 'people'", maxPerPage: 100);

    List<Person> entities = new();
    await foreach (Person person in pageable)
    {
        entities.Add(person);
    }
    return Results.Ok(entities);
});

app.MapGet("/people/{rowKey}", async (string rowKey) =>
{

    TableClient tableClient = await GetTableClientAsync("People");

    var response = await tableClient.GetEntityAsync<Person>("people", rowKey);

    return Results.Ok(response.Value);
});

app.MapDelete("/people/{rowKey}", async (string rowKey) =>
{

    TableClient tableClient = await GetTableClientAsync("People");

    await tableClient.DeleteEntityAsync("people", rowKey);

    return Results.NoContent();
});

app.Run();


async Task<TableClient> GetTableClientAsync(string tableName)
{
    TableServiceClient serviceClient = new(
        endpoint: new Uri("https://<storage-account-name>.table.core.windows.net/"),
        new DefaultAzureCredential()
    );

    TableClient client = serviceClient.GetTableClient(
    tableName: tableName
    );
    await client.CreateIfNotExistsAsync();

    return client;
}

public class Person : ITableEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PartitionKey { get; set; } = null!;
    public string RowKey { get; set; } = Guid.NewGuid().ToString();
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}

public record PersonDto(string FirstName,string LastName);
public record PersonUpdateDto(string RowKey,string FirstName, string LastName);