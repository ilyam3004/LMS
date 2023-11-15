using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Domain.Common;
using MediatR;

namespace Application.Subjects.Commands.RemoveSubject;

public class RemoveSubjectCommandHandler(IUnitOfWork unitOfWork,
        IJwtTokenReader jwtTokenReader)
    : IRequestHandler<RemoveSubjectCommand, Result<List<LecturerSubjectResult>>>
{
    public async Task<Result<List<LecturerSubjectResult>>> Handle(
        RemoveSubjectCommand command,
        CancellationToken cancellationToken)
    {
        if (!await unitOfWork.Subjects.SubjectExists(command.SubjectId))
            return Errors.Subject.SubjectNotFound;

        var userId = jwtTokenReader.ReadUserIdFromToken(command.Token);
        if (userId is null)
            return Errors.User.InvalidToken;
        
        var user = await unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        unitOfWork.Subjects.Remove(command.SubjectId);
        await unitOfWork.SaveChangesAsync();

        return await GetLecturerSubjects(user.Lecturer!.LecturerId);
    }

    private async Task<List<LecturerSubjectResult>> GetLecturerSubjects(Guid lecturerId)
    {
        var lecturerSubjects = await unitOfWork.Subjects
            .GetLecturerSubjects(lecturerId);

        return lecturerSubjects.Select(subject =>
            new LecturerSubjectResult(subject,
                subject.GroupSubjects.FirstOrDefault()!.Group.Name)).ToList();
    }
}