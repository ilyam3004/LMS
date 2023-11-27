using Application.Common.Interfaces.Persistence;
using Application.Models;
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

        var filePath = Path.Combine("/app/uploads", studentTask.FileUrl);

        var fileContent = await File.ReadAllBytesAsync(filePath, cancellationToken);
        
        return new DownloadTaskResult(fileContent, studentTask.OrdinalFileName);
    }
}