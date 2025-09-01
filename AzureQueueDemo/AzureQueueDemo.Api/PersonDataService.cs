
using Azure.Identity;
using Azure.Storage.Queues;
using System.Text.Json;

namespace AzureQueueDemo.Api;

public class PersonDataService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string storageAccountName = "rd003blobstorage";
        string queueName = "person-data";

        QueueClient queueClient = new QueueClient(
            new Uri($"https://{storageAccountName}.queue.core.windows.net/{queueName}"),
            new DefaultAzureCredential());

        while (!stoppingToken.IsCancellationRequested)
        {
            var queueMessage = await queueClient.ReceiveMessageAsync();

            if (queueMessage.Value != null)
            {
                var personData = JsonSerializer.Deserialize<Person>(queueMessage.Value.MessageText);

                // Process that message 

                Console.WriteLine(personData.ToString());


                await queueClient.DeleteMessageAsync(queueMessage.Value.MessageId,queueMessage.Value.PopReceipt);
            }

            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}
