using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models.Authentication;
using Domain.Entities;
using Domain.Common;
using Domain.Enums;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace Application.Authentication.Commands.RegisterStudent;

public class RegisterStudentCommandHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RegisterStudentCommand, Result<AuthenticationResult>>
{
    public async Task<Result<AuthenticationResult>> Handle(
        RegisterStudentCommand command, CancellationToken cancellationToken)
    {
        if (await unitOfWork.Users.UserExistsByEmail(command.Email))
        {
            return Errors.User.DuplicateEmail;
        }

        var group = await unitOfWork.Groups.GetGroupByName(command.GroupName);

        if (group is null)
        {
            return Errors.Group.NotFound;
        }

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Email = command.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(command.Password)
        };

        await unitOfWork.Users.AddAsync(user);

        var student = new Student
        {
            StudentId = Guid.NewGuid(),
            UserId = user.UserId,
            GroupId = group.GroupId,
            FullName = $"{command.FirstName} {command.LastName}",
            Address = command.Address,
            Birthday = command.Birthday.ToUniversalTime(),
        };

        await unitOfWork.GetRepository<Student>().AddAsync(student);
        await unitOfWork.SaveChangesAsync();

        await AddAllTasksToStudent(student, group);

        var token = jwtTokenGenerator.GenerateToken(
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

                await unitOfWork.StudentTasks.AddAsync(studentTask);
            });
        });
        
        await unitOfWork.SaveChangesAsync();
    }
}