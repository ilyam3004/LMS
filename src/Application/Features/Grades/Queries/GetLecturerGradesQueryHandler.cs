using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Grades;
using Application.Models.Tasks;
using Domain.Abstractions.Results;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.Grades.Queries;

public class GetLecturerGradesQueryHandler
    : IRequestHandler<GetLecturerGradesQuery, Result<List<SubjectGradesResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public GetLecturerGradesQueryHandler(IUnitOfWork unitOfWork, IJwtTokenReader jwtTokenReader)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenReader;
    }

    public async Task<Result<List<SubjectGradesResult>>> Handle(GetLecturerGradesQuery query,
        CancellationToken cancellationToken)
    {
        var userId = _jwtTokenReader.ReadUserIdFromToken(query.Token);

        if (userId is null)
            return Errors.Authentication.InvalidToken;

        var user = await _unitOfWork.Users.GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.Authentication.UserNotFound;

        var subjects = await _unitOfWork.Subjects
            .GetLecturerSubjects(user.Lecturer!.LecturerId);

        return subjects.Select(subject =>
        {
            List<StudentTasksResult> studentSubjectTasksResults = [];

            foreach (var student in subject.Group.Students)
            {
                var totalGrade = 0;

                var studentTasks = student.Tasks.Where(t =>
                        t.Task.SubjectId == subject.SubjectId)
                    .Select(uploadedTask =>
                    {
                        totalGrade += uploadedTask.Grade;
                        return new StudentTaskResult(uploadedTask.Task, uploadedTask);
                    }).ToList();

                var averageGrade = 0.0;

                if (subject.Tasks.Count != 0)
                    averageGrade = Convert.ToDouble(totalGrade) / subject.Tasks.Count;

                studentSubjectTasksResults.Add(new StudentTasksResult(
                    student.StudentId, student.FullName, totalGrade, averageGrade,
                    studentTasks.ToList()));
            }

            return new SubjectGradesResult(
                subject.SubjectId,
                subject.Name,
                subject.Group.Name,
                studentSubjectTasksResults);
        }).ToList();
    }

    private async Task<List<SubjectGradesResult>> GetSubjectGradesResults(
        List<Subject> subjects)
    {
        return subjects.Select(subject =>
        {
            List<StudentTasksResult> studentSubjectTasksResults = CalculateStudentTasksResults(subject);

            return new SubjectGradesResult(
                subject.SubjectId,
                subject.Name,
                subject.Group.Name,
                studentSubjectTasksResults);
        }).ToList();
    }

    private List<StudentTasksResult> CalculateStudentTasksResults(Subject subject)
        => subject.Group.Students.Select(student =>
        {
            var studentTasks = CalculateStudentTasks(subject, student);
            var totalGrade = CalculateTotalGrade(studentTasks);
            var averageGrade = CalculateAverageGrade(subject, totalGrade);

            return CreateStudentTasksResult(student, studentTasks, totalGrade, averageGrade);
        }).ToList();

    private List<StudentTaskResult> CalculateStudentTasks(Subject subject, Student student)
        => student.Tasks.Where(t => t.Task.SubjectId == subject.SubjectId)
            .Select(uploadedTask => new StudentTaskResult(uploadedTask.Task, uploadedTask))
            .ToList();

    private int CalculateTotalGrade(List<StudentTaskResult> studentTasks)
        => studentTasks.Sum(task => task.UploadedTask.Grade);

    private double CalculateAverageGrade(Subject subject, int totalGrade)
        => (subject.Tasks.Count != 0) ? (double) totalGrade / subject.Tasks.Count : 0.0;

    private StudentTasksResult CreateStudentTasksResult(Student student,
        List<StudentTaskResult> studentTasks, int totalGrade, double averageGrade)
        => new(student.StudentId,
            student.FullName,
            totalGrade,
            averageGrade,
            studentTasks);

    //     return subjects.Select(subject =>
    //     {
    //         List<StudentTasksResult> studentSubjectTasksResults = [];
    //
    //         foreach (var student in subject.Group.Students)
    //         {
    //             var totalGrade = 0;
    //
    //             var studentTasks = student.Tasks.Where(t =>
    //                     t.Task.SubjectId == subject.SubjectId)
    //                 .Select(uploadedTask =>
    //                 {
    //                     totalGrade += uploadedTask.Grade;
    //                     return new StudentTaskResult(uploadedTask.Task, uploadedTask);
    //                 }).ToList();
    //
    //             var averageGrade = 0.0;
    //
    //             if (subject.Tasks.Count != 0)
    //                 averageGrade = Convert.ToDouble(totalGrade) / subject.Tasks.Count;
    //
    //             studentSubjectTasksResults.Add(new StudentTasksResult(
    //                 student.StudentId, student.FullName, totalGrade, averageGrade,
    //                 studentTasks.ToList()));
    //         }
    //
    //         return new SubjectGradesResult(
    //             subject.SubjectId,
    //             subject.Name,
    //             subject.Group.Name,
    //             studentSubjectTasksResults);
    //     }).ToList();
    // }
}