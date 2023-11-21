using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Domain.Entities;
using Domain.Common;
using MediatR;

namespace Application.Subjects.Commands.CreateSubject;

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
            LecturerId = user.Lecturer!.LecturerId
        };

        var groupSubject = new GroupSubject
        {
            GroupId = group.GroupId,
            SubjectId = subject.SubjectId
        };

        await _unitOfWork.Subjects.AddAsync(subject);
        await _unitOfWork.GetRepository<GroupSubject>().AddAsync(groupSubject);

        await _unitOfWork.SaveChangesAsync();

        return await GetLecturerSubjects(user.Lecturer.LecturerId);
    }

    private async Task<List<LecturerSubjectResult>> GetLecturerSubjects(Guid lecturerId)
    {
        var lecturerSubjects = await _unitOfWork.Subjects
            .GetLecturerSubjects(lecturerId);
        
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

    private bool SubjectExistsInGroup(string subjectName, Group group)
        => group.GroupSubjects.Any(gs => gs.Subject.Name == subjectName);
}