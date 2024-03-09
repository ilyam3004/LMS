using Microsoft.AspNetCore.Http;

namespace Application.Services;

public interface IFileUploader
{
    Task<string> UploadFileAndGetFilePath(IFormFile file);
}