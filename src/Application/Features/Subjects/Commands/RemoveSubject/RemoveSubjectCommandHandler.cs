using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Groups;
using Application.Models.Subjects;
using Domain.Abstractions.Results;
using Domain.Common;
using MediatR;

namespace Application.Features.Subjects.Commands.RemoveSubject;

public class RemoveSubjectCommandHandler
    : IRequestHandler<RemoveSubjectCommand, Result<List<LecturerSubjectResult>>>
{
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSubjectCommandHandler(IUnitOfWork unitOfWork,
        IJwtTokenReader jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenGenerator;
    }

    public async Task<Result<List<LecturerSubjectResult>>> Handle(
        RemoveSubjectCommand command,
        CancellationToken cancellationToken)
    {
        var subject = await _unitOfWork.Subjects
            .GetSubjectByIdWithRelations(command.SubjectId);

        if (subject is null)
            return Errors.Subject.SubjectNotFound;

        var userId = _jwtTokenReader.ReadUserIdFromToken(command.Token);
        if (userId is null)
            return Errors.Authentication.InvalidToken;

        var user = await _unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));

        if (user is null)
            return Errors.Authentication.UserNotFound;

        if (user.Lecturer?.LecturerId != subject.LecturerId)
            return Errors.Subject.LecturerNotOwnerOfSubject;

        _unitOfWork.Subjects.Remove(subject);
        await _unitOfWork.SaveChangesAsync();

        return await GetLecturerSubjects(user.Lecturer!.LecturerId);
    }

    private async Task<List<LecturerSubjectResult>> GetLecturerSubjects(Guid lecturerId)
    {
        var lecturerSubjects = await _unitOfWork.Subjects
            .GetLecturerSubjects(lecturerId);

        return lecturerSubjects.Select(subject =>
        {
            var studentResults = subject.Group.Students.Select(s =>
                new StudentResult(s)).ToList();

            var groupResult = new GroupResult(subject.Group, studentResults);

            return new LecturerSubjectResult(subject, groupResult, []);
        }).ToList();
    }
}