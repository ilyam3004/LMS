using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Tasks.Commands.ReturnTask;

public class ReturnTaskCommandHandler
    : IRequestHandler<ReturnTaskCommand, Result<LecturerTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ReturnTaskCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LecturerTaskResult>> Handle(ReturnTaskCommand command,
        CancellationToken cancellationToken)
    {
        var studentTask = await _unitOfWork.StudentTasks.GetByIdAsync(command.StudentTaskId);

        if (studentTask is null)
            return Errors.Task.TaskNotFound;
        
        if(studentTask.Status != StudentTaskStatus.Uploaded)
            return Errors.Task.TaskNotUploaded;

        studentTask.Status = StudentTaskStatus.Returned;
        studentTask.UploadedAt = null;
        studentTask.FileUrl = null;
        studentTask.Grade = 0;
        
        _unitOfWork.StudentTasks.Update(studentTask);
        
        await _unitOfWork.SaveChangesAsync();
        
        var task = await _unitOfWork.Tasks.GetTaskByIdWithRelations(studentTask.TaskId);

        if (task is null)
            return Errors.Task.TaskNotFound;

        return new LecturerTaskResult(task);
    }
}