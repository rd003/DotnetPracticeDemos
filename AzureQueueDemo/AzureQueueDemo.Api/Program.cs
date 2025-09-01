using Azure.Identity;
using Azure.Storage.Queues;
using AzureQueueDemo.Api;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<PersonDataService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/post",async (Person person) =>
{
    string storageAccountName = "rd003blobstorage";
    string queueName = "person-data";

    QueueClient queueClient = new QueueClient(
        new Uri($"https://{storageAccountName}.queue.core.windows.net/{queueName}"),
        new DefaultAzureCredential());

    string message = JsonSerializer.Serialize(person);
    await queueClient.SendMessageAsync(message);
});

app.Run();


public record Person(string FirstName,string LastName);