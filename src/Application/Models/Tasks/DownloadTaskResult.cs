namespace Application.Models.Tasks;

public record DownloadTaskResult(
    byte[] FileContent,
    string FileName,
    string ContentType);