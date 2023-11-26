using Application.Common.Interfaces.Persistence;
using Application.Models;
using Domain.Abstractions.Results;
using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Tasks.Commands.AcceptTask;

public class AcceptStatusCommandHandler
    : IRequestHandler<AcceptTaskCommand, Result<LecturerTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AcceptStatusCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LecturerTaskResult>> Handle(AcceptTaskCommand command,
        CancellationToken cancellationToken)
    {
        var studentTask = await _unitOfWork.StudentTasks
            .GetByIdAsyncWithRelations(command.StudentTaskId);

        if (studentTask is null)
            return Errors.Task.TaskNotFound;

        if (studentTask.Status != StudentTaskStatus.Uploaded)
            return Errors.Task.TaskNotUploaded;

        if (studentTask.Task.MaxGrade < command.Grade)
            return Errors.Task.GradeTooHigh;

        studentTask.Status = StudentTaskStatus.Accepted;
        studentTask.Grade = command.Grade;

        _unitOfWork.StudentTasks.Update(studentTask);

        await _unitOfWork.SaveChangesAsync();
        
        return await GetTaskResult(studentTask.TaskId);
    }

    private async Task<LecturerTaskResult> GetTaskResult(Guid taskId)
    {
        var task = await _unitOfWork.Tasks.GetTaskByIdWithGroupRelation(taskId);

        return new LecturerTaskResult(task!);
    }
}