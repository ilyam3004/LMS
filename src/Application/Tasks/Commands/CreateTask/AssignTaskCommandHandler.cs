using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Task = Domain.Entities.Task;
using Application.Services;
using Application.Models;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
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

        await AddStudentTasks(task.TaskId);

        return await GetSubjectResult(command.SubjectId);
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

    private async System.Threading.Tasks.Task AddStudentTasks(Guid taskId)
    {
        var task = await _unitOfWork.Tasks.GetTaskByIdWithRelations(taskId);
        var group = await _unitOfWork.Groups.GetGroupByIdWithStudents(task!.Subject.GroupId);
        
        group!.Students.ForEach(s =>
        {
            var studentTask = new StudentTask
            {
                StudentTaskId = Guid.NewGuid(),
                TaskId = task.TaskId,
                StudentId = s.StudentId,
                Status = StudentTaskStatus.NotUploaded
            };

            _unitOfWork.StudentTasks.AddAsync(studentTask);
        });
        
        await _unitOfWork.SaveChangesAsync();
    }
}