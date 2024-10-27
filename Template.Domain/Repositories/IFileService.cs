using Microsoft.AspNetCore.Http;

namespace Template.Domain.Repositories;

public interface IFileService
{
    Task<string?> SaveFileAsync(IFormFile file, string path, string[] allowedFileExtensions);
    void DeleteFile(string fileNameWithExtension);
}