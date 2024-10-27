using Microsoft.AspNetCore.Http;

namespace Template.Domain.Repositories;

public interface IFileService
{
    List<string>? SaveFileAsync(List<IFormFile> file, string path, string[] allowedFileExtensions);
    void DeleteFile(string fileNameWithExtension);
}