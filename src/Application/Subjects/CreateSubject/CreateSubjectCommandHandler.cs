using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models;
using Domain.Abstractions.Results;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Application.Subjects.Commands.CreateSubject;

public class CreateSubjectCommandHandler(IUnitOfWork unitOfWork,
    IJwtTokenReader jwtTokenReader)
    : IRequestHandler<CreateSubjectCommand, Result<List<SubjectResult>>>
{
    public async Task<Result<List<SubjectResult>>> Handle(
        CreateSubjectCommand command, 
        CancellationToken cancellationToken)
    {
        if(await unitOfWork.Subjects.ExistsByName(command.Name))
        {
            return Errors.Subject.SubjectAlreadyExists;
        }

        var group = await unitOfWork.Groups.GetGroupByName(command.GroupName);
        if(group is null)
        {
            return Errors.Group.NotFound;
        }

        var userId = jwtTokenReader.ReadUserIdFromToken(command.Token);

        var user = await unitOfWork.Users.GetByIdAsync(Guid.Parse(userId));
        var subject = new Subject
        {
            SubjectId = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            LecturerId = user.Lecturer.LecturerId
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
        var lecturerSubjects = await unitOfWork.Subjects.GetLecturerSubjects(lecturerId);

        return lecturerSubjects.Select(s => new SubjectResult(s)).ToList();
    }
}
