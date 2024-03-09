using Application.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class FileUploader : IFileUploader
{
    public async Task<string> UploadFileAndGetFilePath(IFormFile file)
    {
        var fileName = $"{file.FileName}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var filePath = Path.Combine("/app/uploads", fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return filePath;
    }
}