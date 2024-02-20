using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Subjects;
using Domain.Abstractions.Results;
using Application.Models.Groups;
using Application.Models.Tasks;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Task = Domain.Entities.Task;

namespace Application.Features.Tasks.Commands.RemoveTask;

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

        var studentResults = MapStudentsToStudentResults(
            subject.Group.Students);

        var groupResult = new GroupResult(subject.Group, studentResults);

        var taskResults = MapTasksToLecturerTaskResults(
            subject.Tasks);

        return new LecturerSubjectResult(subject, groupResult, taskResults);
    }
    
    private List<StudentResult> MapStudentsToStudentResults(List<Student> students)
        => students.Select(s => new StudentResult(s)).ToList();
    
    private List<LecturerTaskResult> MapTasksToLecturerTaskResults(List<Task> tasks)
            => tasks.Select(t => new LecturerTaskResult(t)).ToList();
}