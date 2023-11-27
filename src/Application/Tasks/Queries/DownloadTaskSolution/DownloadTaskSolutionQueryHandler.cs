using Application.Common.Interfaces.Persistence;
using Application.Models;
using Domain.Abstractions.Errors;
using Domain.Abstractions.Results;
using Domain.Common;
using MediatR;

namespace Application.Tasks.Queries.DownloadTaskSolution;

public class DownloadTaskSolutionQueryHandler
    : IRequestHandler<DownloadTaskSolutionQuery, Result<DownloadTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DownloadTaskSolutionQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DownloadTaskResult>> Handle(DownloadTaskSolutionQuery query,
        CancellationToken cancellationToken)
    {
        var studentTask = await _unitOfWork.StudentTasks.GetByIdAsync(query.StudentTaskId);

        if (studentTask is null)
            return Errors.Task.StudentTaskNotFound;

        //var filePath = Path.Combine("/app/uploads", studentTask.FileUrl);
        var fileContent = await File.ReadAllBytesAsync(studentTask.FileUrl, cancellationToken);

        if (studentTask.OrdinalFileName is null)
            return Errors.File.OrdinalFileNameNotFound;

        var contentType = GetContentType(studentTask.OrdinalFileName);


        return new DownloadTaskResult(fileContent, studentTask.OrdinalFileName, contentType);
    }

    private string GetContentType(string fileName)
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