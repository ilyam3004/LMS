using Microsoft.AspNetCore.Http;

namespace Application.Services;

public interface IFileManager
{
    Task<string> UploadFileAndGetFilePath(IFormFile file);
    Task RemoveFile(string? filePath);
    bool FileExists(string? path);
    string GetContentType(string fileName);
}