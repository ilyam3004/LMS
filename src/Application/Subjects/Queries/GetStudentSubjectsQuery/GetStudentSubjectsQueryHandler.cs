using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models;
using Domain.Abstractions.Results;
using Domain.Common;
using MediatR;

namespace Application.Subjects.Queries.GetStudentSubjectsQuery;

public class GetStudentSubjectsQueryHandler(IUnitOfWork unitOfWork,
        IJwtTokenReader jwtTokenReader)
    : IRequestHandler<GetStudentSubjectsQuery, Result<List<StudentSubjectResult>>>
{
    public async Task<Result<List<StudentSubjectResult>>> Handle(
        GetStudentSubjectsQuery query,
        CancellationToken cancellationToken)
    {
        var userId = jwtTokenReader.ReadUserIdFromToken(query.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        var studentSubjects = await unitOfWork.Subjects
            .GetStudentSubjects(user.Student!.GroupId);

        return studentSubjects.Select(subject =>
            new StudentSubjectResult(subject, "test lecturer name"))
            .ToList();
    }
}