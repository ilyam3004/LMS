using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models;
using Application.Models.Groups;
using Application.Models.Subjects;
using Domain.Abstractions.Results;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.Features.Subjects.Commands.CreateSubject;

public class CreateSubjectCommandHandler
    : IRequestHandler<CreateSubjectCommand, Result<List<LecturerSubjectResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public CreateSubjectCommandHandler(IUnitOfWork unitOfWork, IJwtTokenReader jwtTokenReader)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenReader;
    }

    public async Task<Result<List<LecturerSubjectResult>>> Handle(
        CreateSubjectCommand command,
        CancellationToken cancellationToken)
    {
        var group = await _unitOfWork.Groups.GetGroupByName(command.GroupName);
        if (group is null)
            return Errors.Group.NotFound;

        if (SubjectExistsInGroup(command.Name, group))
            return Errors.Subject.SubjectAlreadyExists;

        var userId = _jwtTokenReader.ReadUserIdFromToken(command.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await _unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        var subject = new Subject
        {
            SubjectId = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            LecturerId = user.Lecturer!.LecturerId,
            GroupId = group.GroupId
        };

        await _unitOfWork.Subjects.AddAsync(subject);

        await _unitOfWork.SaveChangesAsync();

        return await GetLecturerSubjects(user.Lecturer.LecturerId);
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

    private bool SubjectExistsInGroup(string subjectName, Group group)
        => group.Subjects.Any(s => s.Name == subjectName);
}