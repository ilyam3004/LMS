using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Domain.Entities;
using Domain.Common;
using MediatR;

namespace Application.Subjects.Commands.CreateSubject;

public class CreateSubjectCommandHandler(IUnitOfWork unitOfWork, 
    IJwtTokenReader jwtTokenReader)
    : IRequestHandler<CreateSubjectCommand, Result<List<SubjectResult>>>
{
    public async Task<Result<List<SubjectResult>>> Handle(
        CreateSubjectCommand command,
        CancellationToken cancellationToken)
    {
        var group = await unitOfWork.Groups.GetGroupByName(command.GroupName);
        if (group is null)
            return Errors.Group.NotFound;

        if (SubjectExistsInGroup(command.Name, group))
            return Errors.Subject.SubjectAlreadyExists;

        var userId = jwtTokenReader.ReadUserIdFromToken(command.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await unitOfWork.Users
        .GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        var subject = new Subject
        {
            SubjectId = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            LecturerId = user.Lecturer!.LecturerId
        };

        var groupSubject = new GroupSubject
        {
            GroupId = group.GroupId,
            SubjectId = subject.SubjectId
        };

        await unitOfWork.Subjects.AddAsync(subject);
        await unitOfWork.GetRepository<GroupSubject>().AddAsync(groupSubject);

        await unitOfWork.SaveChangesAsync();

        return await GetLecturerSubjects(user.Lecturer.LecturerId);
    }

    private async Task<List<SubjectResult>> GetLecturerSubjects(Guid lecturerId)
    {
        var lecturerSubjects = await unitOfWork.Subjects
            .GetLecturerSubjects(lecturerId);

        return lecturerSubjects.Select(subject =>
            new SubjectResult(subject,
                subject.GroupSubjects.FirstOrDefault()!.Group.Name)).ToList();
    }

    private bool SubjectExistsInGroup(string subjectName, Group group)
        => group.GroupSubjects.Any(gs => gs.Subject.Name == subjectName);
}