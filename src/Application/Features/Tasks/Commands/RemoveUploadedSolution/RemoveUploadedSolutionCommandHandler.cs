using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Tasks;
using Domain.Abstractions.Results;
using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Tasks.Commands.RemoveUploadedSolution;

public class RemoveUploadedSolutionCommandHandler
    : IRequestHandler<RemoveUploadedSolutionCommand, Result<StudentTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public RemoveUploadedSolutionCommandHandler(IUnitOfWork unitOfWork, IJwtTokenReader jwtTokenReader)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenReader;
    }

    public async Task<Result<StudentTaskResult>> Handle(RemoveUploadedSolutionCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _jwtTokenReader.ReadUserIdFromToken(command.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await _unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));

        if (user is null)
            return Errors.User.UserNotFound;

        var studentTask = await _unitOfWork.StudentTasks
            .GetByIdAsync(command.StudentTaskId);

        if (studentTask is null)
            return Errors.Task.StudentTaskNotFound;

        if (studentTask.Status is not StudentTaskStatus.Uploaded)
            return Errors.Task.WrongTaskStatus;
        
        if(!File.Exists(studentTask.FileUrl))
            return Errors.File.FileNotFound;

        RemoveUploadedFile(studentTask.FileUrl);
        
        studentTask.FileUrl = null;
        studentTask.OrdinalFileName = null;
        studentTask.Status = StudentTaskStatus.NotUploaded;
        studentTask.UploadedAt = null;

        _unitOfWork.StudentTasks.Update(studentTask);

        await _unitOfWork.SaveChangesAsync();

        return await GetTaskResult(studentTask.TaskId, user.Student.StudentId);
    }

    private async Task<StudentTaskResult> GetTaskResult(Guid taskId, Guid studentId)
    {
        var task = await _unitOfWork.Tasks.GetTaskByIdWithRelations(taskId);

        var studentTask = task.StudentTasks.FirstOrDefault(studentTask =>
            studentTask.StudentId == studentId);

        return new StudentTaskResult(task, studentTask);
    }

    private static async Task RemoveUploadedFile(string filePath)
    {
        if (filePath is null)
            return;

        await using var stream = new FileStream(filePath, FileMode.Open);
        await stream.DisposeAsync();

        File.Delete(filePath);
    }
}