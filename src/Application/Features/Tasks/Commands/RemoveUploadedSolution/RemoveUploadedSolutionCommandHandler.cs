using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models.Tasks;
using Application.Services;
using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Tasks.Commands.RemoveUploadedSolution;

public class RemoveUploadedSolutionCommandHandler
    : IRequestHandler<RemoveUploadedSolutionCommand, Result<StudentTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IFileManager _fileManager;

    public RemoveUploadedSolutionCommandHandler(IUnitOfWork unitOfWork,
        IJwtTokenReader jwtTokenReader,
        IFileManager fileManager)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenReader;
        _fileManager = fileManager;
    }

    public async Task<Result<StudentTaskResult>> Handle(RemoveUploadedSolutionCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _jwtTokenReader.ReadUserIdFromToken(command.Token);
        if (userId is null)
            return Errors.Authentication.InvalidToken;

        var user = await _unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));

        if (user is null)
            return Errors.Authentication.UserNotFound;

        var studentTask = await _unitOfWork.StudentTasks
            .GetByIdAsync(command.StudentTaskId);

        if (studentTask is null)
            return Errors.Task.StudentTaskNotFound;

        if (studentTask.Status is not StudentTaskStatus.Uploaded)
            return Errors.Task.WrongTaskStatus;

        if (!_fileManager.FileExists(studentTask.FileUrl))
            return Errors.File.FileNotFound;

        await _fileManager.RemoveFile(studentTask.FileUrl);

        studentTask.FileUrl = null;
        studentTask.OrdinalFileName = null;
        studentTask.Status = StudentTaskStatus.NotUploaded;
        studentTask.UploadedAt = null;

        _unitOfWork.StudentTasks.Update(studentTask);

        await _unitOfWork.SaveChangesAsync();

        return await GetTaskResult(studentTask.TaskId, user.Student!.StudentId);
    }

    private async Task<StudentTaskResult> GetTaskResult(Guid taskId, Guid studentId)
    {
        var task = await _unitOfWork.Tasks.GetTaskByIdWithRelations(taskId);

        var studentTask = task!.StudentTasks.FirstOrDefault(studentTask =>
            studentTask.StudentId == studentId);

        return new StudentTaskResult(task, studentTask!);
    }
}