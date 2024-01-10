using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Tasks;
using Application.Services;
using Domain.Abstractions.Results;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tasks.Commands.CreateComment;

public class CommentTaskCommandHandler
    : IRequestHandler<CommentTaskCommand, Result<UploadedTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CommentTaskCommandHandler(IUnitOfWork unitOfWork,
        IJwtTokenReader jwtTokenReader,
        IDateTimeProvider dateTimeProvider)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenReader;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<UploadedTaskResult>> Handle(CommentTaskCommand taskCommand,
        CancellationToken cancellationToken)
    {
        var userId = _jwtTokenReader.ReadUserIdFromToken(taskCommand.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await _unitOfWork.Users.GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        var studentTask = await _unitOfWork.StudentTasks.GetByIdAsyncWithRelations(taskCommand.StudentTaskId);
        if (studentTask is null)
            return Errors.Task.StudentTaskNotFound;

        var taskComment = new TaskComment
        {
            TaskCommentId = Guid.NewGuid(),
            CreatedAt = _dateTimeProvider.UtcNow,
            UserId = user.UserId,
            Comment = taskCommand.Comment,
            StudentTaskId = taskCommand.StudentTaskId
        };

        await _unitOfWork.GetRepository<TaskComment>().AddAsync(taskComment);

        await _unitOfWork.SaveChangesAsync();

        return await GetUploadedTaskResult(studentTask);
    }

    private async Task<UploadedTaskResult> GetUploadedTaskResult(StudentTask studentTask)
    {
        var task = await _unitOfWork.Tasks.GetTaskByIdWithRelations(studentTask.TaskId);

        var uploadedStudentTask = task!.StudentTasks.FirstOrDefault(studTaskFromDb =>
            studTaskFromDb.StudentId == studentTask.StudentId);

        return new UploadedTaskResult(uploadedStudentTask!);
    }
}