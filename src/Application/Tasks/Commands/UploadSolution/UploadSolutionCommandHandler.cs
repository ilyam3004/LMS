﻿using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Application.Services;
using Domain.Common;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Tasks.Commands.UploadSolution;

public class UploadSolutionCommandHandler
    : IRequestHandler<UploadSolutionCommand, Result<StudentTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IJwtTokenReader _jwtTokenReader;

    public UploadSolutionCommandHandler(IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IJwtTokenReader jwtTokenReader)
    {
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _jwtTokenReader = jwtTokenReader;
    }

    public async Task<Result<StudentTaskResult>> Handle(UploadSolutionCommand command,
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
            .GetByIdAsync(command.studentTaskId);

        if (studentTask is null)
            return Errors.Task.StudentTaskNotFound;

        if (command.File == null || command.File.Length == 0)
            return Errors.File.FileNotFound;

        var filePath = await UploadFileAndGetFilePath(command.File);
        
        studentTask.FileUrl = filePath;
        studentTask.Status = StudentTaskStatus.Uploaded;
        studentTask.UploadedAt = _dateTimeProvider.UtcNow;
        
        _unitOfWork.StudentTasks.Update(studentTask);

        await _unitOfWork.SaveChangesAsync();

        return await GetTaskResult(studentTask.TaskId, user.Student.StudentId);
    }

    private async Task<StudentTaskResult> GetTaskResult(Guid taskId, Guid studentId)
    {
        var task = await _unitOfWork.Tasks.GetTaskByIdWithLecturerRelation(taskId);

        var studentTask = task.StudentTasks.FirstOrDefault(studentTask =>
            studentTask.StudentId == studentId);

        return new StudentTaskResult(task, studentTask);
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