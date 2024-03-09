using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models.Tasks;
using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Tasks.Commands.RejectTask;

public class RejectTaskCommandHandler
    : IRequestHandler<RejectTaskCommand, Result<LecturerTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public RejectTaskCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<LecturerTaskResult>> Handle(RejectTaskCommand command,
        CancellationToken cancellationToken)
    {
        var studentTask = await _unitOfWork.StudentTasks
            .GetByIdAsyncWithRelations(command.StudentTaskId);

        if (studentTask is null)
            return Errors.Task.StudentTaskNotFound;

        if (studentTask.Task.Deadline >= DateTime.UtcNow)
            return Errors.Task.TaskDeadlineNotExpired;
        
        if(studentTask.Status is StudentTaskStatus.Rejected 
           or StudentTaskStatus.Accepted or StudentTaskStatus.Uploaded)
            return Errors.Task.WrongTaskStatus;

        studentTask.Status = StudentTaskStatus.Rejected;
        studentTask.UploadedAt = null;
        studentTask.FileUrl = null;
        studentTask.OrdinalFileName = null;
        studentTask.Grade = 0;

        _unitOfWork.StudentTasks.Update(studentTask);

        await _unitOfWork.SaveChangesAsync();
        
        var updatedTask = await _unitOfWork.Tasks.GetTaskByIdWithRelations(studentTask.TaskId);

        return new LecturerTaskResult(updatedTask!);
    }
}