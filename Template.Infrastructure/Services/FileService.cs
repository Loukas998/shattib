using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Template.Infrastructure.Services;

// Note: This Class Should not be used in any Repository Because It has nothing to do with database operations
public class FileService
{
    /// <summary>
    ///     Note::
    ///     path: The relative path to Images/{path}
    /// </summary>
    public static string? SaveFileAsync(IFormFile file, IWebHostEnvironment environment, string path,
        string[] allowedFileExtensions)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        var prefixedPath = $"Images/{path}";

        if (file == null) throw new ArgumentNullException(nameof(file));
        var extension = Path.GetExtension(file.FileName);
        if (!allowedFileExtensions.Contains(extension))
            throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");

        var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
        var filePath = Path.Combine(environment.ContentRootPath, prefixedPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyToAsync(stream);
        }

        return Path.Combine(prefixedPath, fileName);
    }

    public static void DeleteFile(string path)
    {
        if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

        if (!File.Exists(path)) throw new FileNotFoundException("Invalid file path");
        File.Delete(path);
    }
}