using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Task = Domain.Entities.Task;
using Application.Services;
using Application.Models;
using Domain.Common;
using MediatR;

namespace Application.Tasks.Commands.CreateTask;

public class AssignTaskCommandHandler
    : IRequestHandler<AssignTaskCommand, Result<LecturerSubjectResult>>
{
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public AssignTaskCommandHandler(IJwtTokenReader jwtTokenReader,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _jwtTokenReader = jwtTokenReader;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<LecturerSubjectResult>> Handle(AssignTaskCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Subjects.SubjectExists(command.SubjectId))
            return Errors.Subject.SubjectNotFound;

        var task = new Task
        {
            TaskId = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            SubjectId = command.SubjectId,
            CreatedAt = _dateTimeProvider.UtcNow,
            Deadline = command.Deadline,
            MaxGrade = command.MaxGrade
        };

        await _unitOfWork.Tasks.AddAsync(task);

        await _unitOfWork.SaveChangesAsync();

        return await GetSubjectResult(command.SubjectId);
    }

    private async Task<LecturerSubjectResult> GetSubjectResult(Guid subjectId)
    {
        var subject = await _unitOfWork.Subjects
            .GetSubjectByIdWithRelations(subjectId);

        var groupResults = subject.GroupSubjects.Select(gs =>
        {
            var studentResults = gs.Group.Students.Select(s =>
                new StudentResult(s)).ToList();

            return new GroupResult(gs.Group, studentResults);
        }).ToList();

        var taskResults = subject.Tasks.Select(t =>
            new TaskResult(t)).ToList();
        
        return new LecturerSubjectResult(subject, groupResults, taskResults);
    }
}