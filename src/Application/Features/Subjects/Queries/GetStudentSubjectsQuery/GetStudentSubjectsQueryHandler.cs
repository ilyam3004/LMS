using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Subjects;
using Domain.Abstractions.Results;
using Task = Domain.Entities.Task;
using Application.Models.Tasks;
using Domain.Entities;
using Domain.Common;
using MediatR;

namespace Application.Features.Subjects.Queries.GetStudentSubjectsQuery;

public class GetStudentSubjectsQueryHandler
    : IRequestHandler<GetStudentSubjectsQuery, Result<List<StudentSubjectResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public GetStudentSubjectsQueryHandler(IUnitOfWork unitOfWork,
        IJwtTokenReader jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenGenerator;
    }

    public async Task<Result<List<StudentSubjectResult>>> Handle(
        GetStudentSubjectsQuery query,
        CancellationToken cancellationToken)
    {
        var userId = _jwtTokenReader.ReadUserIdFromToken(query.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await _unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        var studentSubjects = await _unitOfWork.Subjects
            .GetStudentSubjectsWithRelations(user.Student!.GroupId);

        return MapSubjectsToStudentSubjectResults(studentSubjects,
            user.Student.StudentId);
    }

    private List<StudentSubjectResult> MapSubjectsToStudentSubjectResults(
        List<Subject> subjects, Guid studentId)
        => subjects.Select(subject =>
                MapSubjectToStudentSubjectResult(subject, studentId))
            .ToList();

    private StudentSubjectResult MapSubjectToStudentSubjectResult(Subject subject,
        Guid studentId)
    {
        if (subject.Tasks.Count == 0)
            return new StudentSubjectResult(subject,
                [], 0, 0);

        var (taskResults, totalGrade) = MapTasksToTaskResultsAndCalculateTotalGrade(
            subject.Tasks, studentId);

        var averageGrade = CalculateAverageGrade(subject.Tasks.Count, totalGrade);

        return new StudentSubjectResult(subject, taskResults, averageGrade, totalGrade);
    }

    private (List<StudentTaskResult>, int) MapTasksToTaskResultsAndCalculateTotalGrade(
        List<Task> tasks, Guid studentId)
    {
        var totalGrade = 0;

        var taskResults = tasks.Select(task =>
        {
            var studentTask = task.StudentTasks.FirstOrDefault(studentTask =>
                studentTask.StudentId == studentId);

            totalGrade += studentTask.Grade;

            return new StudentTaskResult(task, studentTask);
        }).ToList();

        return (taskResults, totalGrade);
    }

    private double CalculateAverageGrade(int tasksCount, int totalGrade)
        => Math.Round(Convert.ToDouble(totalGrade) / tasksCount, 2);
}