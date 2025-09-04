using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobDemo.Services;


public class BlobStorageServie : IBlobStorageService
{
    private const string ContainerName="images";
    private readonly IConfiguration _config;
    private readonly string _storageAccountName;

    public BlobStorageServie(IConfiguration config)
    {
        _config = config;
        _storageAccountName = _config.GetSection("StorageAccountName").Value ?? throw new InvalidOperationException("No storage account key is provided");
    }

     public async Task<string> UploadFileAsync(IFormFile file, string? fileNameWithExtension = "")
    {
        if (string.IsNullOrEmpty(fileNameWithExtension))
        {
            var fileExtension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            fileNameWithExtension = Guid.NewGuid().ToString("N") + fileExtension;
        }

       // create container  
        var blobServiceClient = new BlobServiceClient(
                new Uri($"https://{_storageAccountName}.blob.core.windows.net"),
                new DefaultAzureCredential());

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        BlobClient blobClient = containerClient.GetBlobClient(fileNameWithExtension);

       await using var stream = file.OpenReadStream();

        await blobClient.UploadAsync(stream, true);

        return blobClient.Uri.ToString();
    }

    public async Task DeleteBlobAsync(string blobUrl)
    {
        string blobName = Path.GetFileName(blobUrl);
        if (string.IsNullOrEmpty(blobName))
        {
            throw new InvalidOperationException("Invalid blob url");
        }

        var blobServiceClient = new BlobServiceClient(
                new Uri($"https://{_storageAccountName}.blob.core.windows.net"),
                new DefaultAzureCredential());

        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync(snapshotsOption: DeleteSnapshotsOption.IncludeSnapshots);
    }

}

