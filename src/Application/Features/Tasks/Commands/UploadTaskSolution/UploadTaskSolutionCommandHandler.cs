using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Microsoft.AspNetCore.Http;
using Application.Models.Tasks;
using Application.Services;
using Domain.Common;
using Domain.Enums;
using MediatR;

namespace Application.Features.Tasks.Commands.UploadTaskSolution;

public class UploadTaskSolutionCommandHandler
    : IRequestHandler<UploadTaskSolutionCommand, Result<StudentTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IJwtTokenReader _jwtTokenReader;

    public UploadTaskSolutionCommandHandler(IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IJwtTokenReader jwtTokenReader)
    {
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _jwtTokenReader = jwtTokenReader;
    }

    public async Task<Result<StudentTaskResult>> Handle(UploadTaskSolutionCommand command,
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

        if (studentTask.Status == StudentTaskStatus.Rejected)
            return Errors.Task.WrongTaskStatus;

        if (command.File?.Length == 0)
            return Errors.File.FileNotFound;

        var filePath = await UploadFileAndGetFilePath(command.File!);

        studentTask.FileUrl = filePath;
        studentTask.OrdinalFileName = command.File?.FileName;
        studentTask.Status = StudentTaskStatus.Uploaded;
        studentTask.UploadedAt = _dateTimeProvider.UtcNow;

        _unitOfWork.StudentTasks.Update(studentTask);

        await _unitOfWork.SaveChangesAsync();

        return await GetTaskResult(studentTask.TaskId, user.Student!.StudentId);
    }

    private async Task<StudentTaskResult> GetTaskResult(Guid taskId, Guid studentId)
    {
        var task = await _unitOfWork.Tasks.GetTaskByIdWithRelations(taskId);

        var studentTask = task?.StudentTasks.FirstOrDefault(studentTask =>
            studentTask.StudentId == studentId);

        return new StudentTaskResult(task!, studentTask!);
    }

    private static async Task<string> UploadFileAndGetFilePath(IFormFile file)
    {
        var fileName = $"{file.FileName}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var filePath = Path.Combine("/app/uploads", fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return filePath;
    }
}