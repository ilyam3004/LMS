using Application.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class FileManager : IFileManager
{
    public async Task<string> UploadFileAndGetFilePath(IFormFile file)
    {
        var fileName = $"{file.FileName}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var filePath = Path.Combine("/app/uploads", fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return filePath;
    }

    public async Task RemoveFile(string? filePath)
    {
        if (filePath is null)
            return;

        await using var stream = new FileStream(filePath, FileMode.Open);

        File.Delete(filePath);
    }

    public bool FileExists(string? path)
        => File.Exists(path);

    public string GetContentType(string fileName)
    {
        var lastDotIndex = fileName.LastIndexOf('.');

        if (lastDotIndex == -1)
            return "application/octet-stream";

        var fileExtension = fileName[(lastDotIndex + 1)..].ToLower();

        return fileExtension switch {
            "txt" => "text/plain",
            "pdf" => "application/pdf",
            "doc" => "application/vnd.ms-word",
            "docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document", 
            "jpeg" => "image/jpeg",
            "jpg" => "image/jpeg", 
            "csv" => "text/csv", 
            "json" => "application/json",
            "xls" => "application/vnd.ms-excel", 
            "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            _ => "application/octet-stream"
        };
    }
}