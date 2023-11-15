using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Domain.Common;
using MediatR;

namespace Application.Subjects.Commands.GetLecturerSubjects;

public class GetLecturerSubjectCommandHandler(IUnitOfWork unitOfWork,
        IJwtTokenReader jwtTokenReader)
    : IRequestHandler<GetLecturerSubjectsCommand, Result<List<SubjectResult>>>
{
    public async Task<Result<List<SubjectResult>>> Handle(
        GetLecturerSubjectsCommand command,
        CancellationToken cancellationToken)
    {
        var userId = jwtTokenReader.ReadUserIdFromToken(command.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        var lecturerSubjects = await unitOfWork.Subjects
            .GetLecturerSubjects(user.Lecturer!.LecturerId);

        return lecturerSubjects.Select(subject =>
            new SubjectResult(subject,
                subject.GroupSubjects.FirstOrDefault()!.Group.Name)).ToList();
    }
}