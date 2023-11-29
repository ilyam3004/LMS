using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Grades;
using Application.Models.Tasks;
using Domain.Abstractions.Results;
using Domain.Common;
using MediatR;

namespace Application.Grades.Queries;

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
            return Errors.User.InvalidToken;

        var user = await _unitOfWork.Users.GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        var subjects = await _unitOfWork.Subjects
            .GetLecturerSubjects(user.Lecturer!.LecturerId);

        var subjectGradesResults = subjects.Select(subject =>
        {
            var studentTasksResults = subject.Group.Students
                .Select(student =>
                    new StudentTasksResult(
                        student.StudentId,
                        student.FullName,
                        student.Tasks.Select(task =>
                            new UploadedTaskResult(task)).ToList())).ToList();

            return new SubjectGradesResult(
                subject.SubjectId,
                subject.Name,
                subject.Group.Name,
                studentTasksResults);
            
        }).ToList();

        return subjectGradesResults;
    }
}