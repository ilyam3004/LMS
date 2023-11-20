using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Domain.Common;
using MediatR;

namespace Application.Subjects.Queries.GetLecturerSubjects;

public class GetLecturerSubjectQueryHandler(
    IUnitOfWork unitOfWork,
    IJwtTokenReader jwtTokenReader)
    : IRequestHandler<GetLecturerSubjectsQuery, Result<List<LecturerSubjectResult>>>
{
    public async Task<Result<List<LecturerSubjectResult>>> Handle(
        GetLecturerSubjectsQuery query,
        CancellationToken cancellationToken)
    {
        var userId = jwtTokenReader.ReadUserIdFromToken(query.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        var lecturerSubjects = await unitOfWork.Subjects
            .GetLecturerSubjects(user.Lecturer!.LecturerId);
       
        return lecturerSubjects.Select(subject =>
        {
            var groupResults = subject.GroupSubjects.Select(gs =>
            {
                var studentResults = gs.Group.Students.Select(s => 
                    new StudentResult(s)).ToList();
                
                return new GroupResult(gs.Group, studentResults);
            }).ToList();

            return new LecturerSubjectResult(subject, groupResults);
        }).ToList();
    }
}