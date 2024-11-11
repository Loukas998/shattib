using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Template.Domain.Repositories;
using Template.Infrastructure.Configuration;

namespace Template.Infrastructure.Services;

public class BlobStorageFileService(IOptions<AzureBlobSettings> settings) : IFileService
{
    private readonly BlobServiceClient _blobServiceClient = new(settings.Value.ConnectionString);
    private readonly string _containerName = settings.Value.ContainerName;

    public List<string>? SaveFiles(List<IFormFile> files, string path, string[] allowedFileExtensions)
    {
        var savedFileUrls = new List<string>();

        foreach (var file in files)
        {
            if (!IsAllowedExtension(file, allowedFileExtensions)) continue;

            var fileUrl = SaveFile(file, path, allowedFileExtensions);
            if (!string.IsNullOrEmpty(fileUrl)) savedFileUrls.Add(fileUrl);
        }

        return savedFileUrls;
    }

    public string SaveFile(IFormFile file, string path, string[] allowedFileExtensions)
    {
        if (!IsAllowedExtension(file, allowedFileExtensions)) return string.Empty;

        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        blobContainerClient.CreateIfNotExists(PublicAccessType.Blob);

        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var blobClient = blobContainerClient.GetBlobClient($"{path}/{uniqueFileName}");

        using (var stream = file.OpenReadStream())
        {
            blobClient.Upload(stream, new BlobHttpHeaders { ContentType = file.ContentType });
        }

        return blobClient.Uri.ToString();
    }

    public async Task<string> SaveFileAsync(IFormFile file, string path, string[] allowedFileExtensions)
    {
        if (!IsAllowedExtension(file, allowedFileExtensions)) return string.Empty;

        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var blobClient = blobContainerClient.GetBlobClient($"{path}/{uniqueFileName}");

        await using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
        }

        return blobClient.Uri.ToString();
    }

    public void DeleteFile(string fileNameWithExtension)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = blobContainerClient.GetBlobClient(fileNameWithExtension);

        blobClient.DeleteIfExists();
    }

    private bool IsAllowedExtension(IFormFile file, string[] allowedFileExtensions)
    {
        var fileExtension = Path.GetExtension(file.FileName)?.ToLower();
        return allowedFileExtensions.Contains(fileExtension);
    }
}