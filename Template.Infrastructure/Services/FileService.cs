using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Template.Domain.Repositories;

namespace Template.Infrastructure.Services;

public class FileService(IWebHostEnvironment environment) : IFileService
{
    public async Task<string>? SaveFileAsync(IFormFile file, string path, string[] allowedFileExtensions)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        var prefixedPath = $"Images/{path}";

        if (file == null) throw new ArgumentNullException(nameof(file));
        var extension = Path.GetExtension(file.FileName);
        if (!allowedFileExtensions.Contains(extension))
            throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");

        var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
        var filePath = Path.Combine(environment.ContentRootPath, prefixedPath, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Path.Combine(prefixedPath, fileName);
    }

    public void DeleteFile(string path)
    {
        if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

        if (!File.Exists(path)) throw new FileNotFoundException("Invalid file path");
        File.Delete(path);
    }
}