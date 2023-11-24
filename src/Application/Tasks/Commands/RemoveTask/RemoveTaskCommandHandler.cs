﻿using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Domain.Common;
using MediatR;

namespace Application.Tasks.Commands.RemoveTask;

public class RemoveTaskCommandHandler
    : IRequestHandler<RemoveTaskCommand, Result<LecturerSubjectResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public RemoveTaskCommandHandler(IUnitOfWork unitOfWork, IJwtTokenReader jwtTokenReader)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenReader;
    }

    public async Task<Result<LecturerSubjectResult>> Handle(
        RemoveTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(command.TaskId);
        if (task is null)
            return Errors.Task.TaskNotFound;

        _unitOfWork.Tasks.Remove(task);
        await _unitOfWork.SaveChangesAsync();

        return await GetSubjectResult(task.SubjectId);
    }

    private async Task<LecturerSubjectResult> GetSubjectResult(Guid subjectId)
    {
        var subject = await _unitOfWork.Subjects
            .GetSubjectByIdWithRelations(subjectId);

        var studentResults = subject!.Group.Students.Select(s =>
            new StudentResult(s)).ToList();

        var groupResult = new GroupResult(subject.Group, studentResults);

        var studentTaskResults = subject.Tasks
            .SelectMany(task => task.StudentTasks
                .Select(st => new StudentTaskResult(st)))
            .ToList();

        var taskResults = subject.Tasks.Select(task =>
            new TaskResult(task, studentTaskResults, subject.Group.Name)).ToList();

        return new LecturerSubjectResult(subject, groupResult, taskResults);
    }
}