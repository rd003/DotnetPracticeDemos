using Azure.Storage.Blobs;
using AzureBlobDemo.Options;
using Microsoft.Extensions.Options;

namespace AzureBlobDemo.Services;

public class BlobStorageServie : IBlobStorageService
{
    private readonly BlobStorageOptions _blobStorage;

    public BlobStorageServie(IOptions<BlobStorageOptions> options)
    {
        _blobStorage = options.Value;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string? fileNameWithExtension = "")
    {
        if (string.IsNullOrEmpty(fileNameWithExtension))
        {
            var fileExtension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            fileNameWithExtension = Guid.NewGuid().ToString("N") + fileExtension;
        }
        BlobContainerClient containerClient = new(_blobStorage.ConnectionString, _blobStorage.ContainerName);

        await containerClient.CreateIfNotExistsAsync();

        await containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

        BlobClient blobClient = containerClient.GetBlobClient(fileNameWithExtension);

        await using var stream = file.OpenReadStream();

        await blobClient.UploadAsync(stream, true);

        return blobClient.Uri.ToString();
    }

}

