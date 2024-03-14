using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models.Tasks;
using Application.Services;
using Domain.Common;
using MediatR;

namespace Application.Features.Tasks.Queries.DownloadTaskSolution;

public class DownloadTaskSolutionQueryHandler
    : IRequestHandler<DownloadTaskSolutionQuery, Result<DownloadTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileManager _fileManager;

    public DownloadTaskSolutionQueryHandler(IUnitOfWork unitOfWork,
        IFileManager fileManager)
    {
        _unitOfWork = unitOfWork;
        _fileManager = fileManager;
    }

    public async Task<Result<DownloadTaskResult>> Handle(DownloadTaskSolutionQuery query,
        CancellationToken cancellationToken)
    {
        var studentTask = await _unitOfWork.StudentTasks.GetByIdAsync(query.StudentTaskId);

        if (studentTask is null)
            return Errors.Task.StudentTaskNotFound;

        if (!_fileManager.FileExists(studentTask.FileUrl))
            return Errors.File.FileNotFound;

        if (studentTask.OrdinalFileName is null)
            return Errors.File.OrdinalFileNameNotFound;

        var fileContent = await _fileManager.ReadFileAsArrayOfBytes(studentTask.FileUrl!);

        var contentType = _fileManager.GetContentType(studentTask.OrdinalFileName);

        return new DownloadTaskResult(fileContent,
            studentTask.OrdinalFileName,
            contentType);
    }
}