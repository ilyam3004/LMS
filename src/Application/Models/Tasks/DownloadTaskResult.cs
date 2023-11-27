namespace Application.Models;

public record DownloadTaskResult(
    byte[] FileContent,
    string FileName);