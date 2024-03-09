using Application.Authentication.Commands.RegisterStudent;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Authentication;
using Domain.Abstractions.Results;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace Application.Features.Authentication.Commands.RegisterStudent;

public class RegisterStudentCommandHandler 
    : IRequestHandler<RegisterStudentCommand, Result<AuthenticationResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    public RegisterStudentCommandHandler(IUnitOfWork unitOfWork, 
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<Result<AuthenticationResult>> Handle(
        RegisterStudentCommand command, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Users.UserExistsByEmail(command.Email))
        {
            return Errors.Authentication.DuplicateEmail;
        }

        var group = await _unitOfWork.Groups.GetGroupByName(command.GroupName);

        if (group is null)
        {
            return Errors.Group.GroupNotFound;
        }

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Email = command.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(command.Password)
        };

        await _unitOfWork.Users.AddAsync(user);

        var student = new Student
        {
            StudentId = Guid.NewGuid(),
            UserId = user.UserId,
            GroupId = group.GroupId,
            FullName = $"{command.FirstName} {command.LastName}",
            Address = command.Address,
            Birthday = command.Birthday.ToUniversalTime(),
        };

        await _unitOfWork.GetRepository<Student>().AddAsync(student);
        await _unitOfWork.SaveChangesAsync();

        await AddAllTasksToStudent(student, group);

        var token = _jwtTokenGenerator.GenerateToken(
            user.UserId,
            student.FullName,
            command.Email,
            Roles.Student);

        return new AuthenticationResult(
            user,
            token);
    }

    private async Task AddAllTasksToStudent(Student student, Group group)
    {
        group.Subjects.ForEach(subject =>
        {
            subject.Tasks.ForEach(async task =>
            {
                var studentTask = new StudentTask
                {
                    StudentTaskId = Guid.NewGuid(),
                    TaskId = task.TaskId,
                    StudentId = student.StudentId,
                    Status = StudentTaskStatus.NotUploaded,
                    UploadedAt = null,
                    FileUrl = null,
                    Grade = 0
                };

                await _unitOfWork.StudentTasks.AddAsync(studentTask);
            });
        });
        
        await _unitOfWork.SaveChangesAsync();
    }
}