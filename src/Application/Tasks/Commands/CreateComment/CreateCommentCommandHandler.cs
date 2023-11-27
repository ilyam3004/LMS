using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Tasks;
using Application.Services;
using Domain.Abstractions.Errors;
using Domain.Abstractions.Results;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Tasks.Commands.CreateComment;

public class CreateCommentCommandHandler
    : IRequestHandler<CreateCommentCommand, Result<StudentTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateCommentCommandHandler(IUnitOfWork unitOfWork,
        IJwtTokenReader jwtTokenReader,
        IDateTimeProvider dateTimeProvider)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenReader;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<StudentTaskResult>> Handle(CreateCommentCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _jwtTokenReader.ReadUserIdFromToken(command.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await _unitOfWork.Users.GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        var studentTask = await _unitOfWork.StudentTasks.GetByIdAsyncWithRelations(command.StudentTaskId);
        if (studentTask is null)
            return Errors.Task.StudentTaskNotFound;

        var taskComment = new TaskComment
        {
            TaskCommentId = Guid.NewGuid(),
            CreatedAt = _dateTimeProvider.UtcNow,
            UserId = user.UserId,
            Comment = command.Comment,
            StudentTaskId = command.StudentTaskId
        };

        await _unitOfWork.GetRepository<TaskComment>().AddAsync(taskComment);

        await _unitOfWork.SaveChangesAsync();

        return await GetStudentTaskResult(studentTask);
    }

    private async Task<StudentTaskResult> GetStudentTaskResult(StudentTask studentTask)
    {
        var task = await _unitOfWork.Tasks.GetTaskByIdWithLecturerRelation(studentTask.TaskId);

        var updatedStudentTask = task.StudentTasks.FirstOrDefault(studentTask =>
            studentTask.StudentId == studentTask.StudentId);

        return new StudentTaskResult(task, updatedStudentTask);
    }
}