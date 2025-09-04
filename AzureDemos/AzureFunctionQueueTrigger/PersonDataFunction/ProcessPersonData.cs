using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace PersonDataFunction;

public class ProcessPersonData
{
    private readonly ILogger<ProcessPersonData> _logger;

    public ProcessPersonData(ILogger<ProcessPersonData> logger)
    {
        _logger = logger;
    }

    [Function(nameof(ProcessPersonData))]
    public void Run([QueueTrigger("person-data", Connection = "StorageConnection")] QueueMessage message)
    {
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message.MessageText);
    }
}