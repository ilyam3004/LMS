using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Subjects;
using Application.Models.Tasks;
using Domain.Abstractions.Results;
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

        return studentSubjects.Select(subject =>
        {
            var totalGrade = 0;

            var taskResults = subject.Tasks.Select(task =>
            {
                var studentTask = task.StudentTasks.FirstOrDefault(studentTask =>
                    studentTask.StudentId == user.Student!.StudentId);

                if (studentTask is null)
                    return new StudentTaskResult(task, studentTask!);

                totalGrade += studentTask.Grade;

                return new StudentTaskResult(task, studentTask);
            }).ToList();

            var averageGrade = 0.0;

            if (subject.Tasks.Count != 0)
                averageGrade = Convert.ToDouble(totalGrade) / subject.Tasks.Count;

            return new StudentSubjectResult(subject, taskResults,
                Math.Round(averageGrade, 2), totalGrade);
        }).ToList();
    }
}