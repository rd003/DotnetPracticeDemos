// Sender
using Azure.Messaging.ServiceBus;
using Azure.Identity;
using System.Text.Json;

var clientOptions = new ServiceBusClientOptions
{
    TransportType = ServiceBusTransportType.AmqpWebSockets
};

ServiceBusClient client = new ServiceBusClient(

    "rd003servicebus.servicebus.windows.net",
    new DefaultAzureCredential(),
    clientOptions);
ServiceBusSender sender = client.CreateSender("MyQueue");

try
{
    Person person = new("Ravindra","Devrani");
    string jsonData = JsonSerializer.Serialize(person);
    ServiceBusMessage message = new(jsonData);
    await sender.SendMessageAsync(message);
    Console.WriteLine($"====> Message has been published to the queue.");
}
finally
{
   await sender.DisposeAsync();
    await client.DisposeAsync();
}

Console.WriteLine("Press any key to end the application");
Console.ReadKey();


record Person(string FirstName, string LastName);