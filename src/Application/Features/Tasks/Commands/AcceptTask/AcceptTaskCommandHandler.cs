using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models.Tasks;
using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Tasks.Commands.AcceptTask;

public class AcceptTaskCommandHandler
    : IRequestHandler<AcceptTaskCommand, Result<LecturerTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AcceptTaskCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LecturerTaskResult>> Handle(AcceptTaskCommand command,
        CancellationToken cancellationToken)
    {
        var studentTask = await _unitOfWork.StudentTasks
            .GetByIdAsyncWithRelations(command.StudentTaskId);

        if (studentTask is null)
            return Errors.Task.StudentTaskNotFound;

        if (studentTask.Status != StudentTaskStatus.Uploaded 
            && studentTask.Status != StudentTaskStatus.Accepted)
            return Errors.Task.StudentTaskNotUploaded;

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
        var task = await _unitOfWork.Tasks.GetTaskByIdWithRelations(taskId);

        return new LecturerTaskResult(task!);
    }
}