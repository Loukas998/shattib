using Microsoft.AspNetCore.Http;

namespace Template.Domain.Repositories;

public interface IFileService
{
    List<string>? SaveFilesAsync(List<IFormFile> file, string path, string[] allowedFileExtensions);
	string SaveFileAsync(IFormFile file, string path, string[] allowedFileExtensions);
	void DeleteFile(string fileNameWithExtension);
}