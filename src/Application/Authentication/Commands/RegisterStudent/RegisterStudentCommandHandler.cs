using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Domain.Entities;
using Domain.Common;
using MediatR;

namespace Application.Authentication.Commands.RegisterStudent;

public class RegisterStudentCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork)
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
            StudentId= Guid.NewGuid(),
            UserId = user.UserId,
            GroupId = group.GroupId,
            FullName = $"{command.FirstName} {command.LastName}",
            Address = command.Address,
            Birthday = command.Birthday.ToUniversalTime()
        };

        await unitOfWork.GetRepository<Student>().AddAsync(student);
        await unitOfWork.SaveChangesAsync();

        var token = jwtTokenGenerator.GenerateToken(
            user.UserId,
            student.FullName,
            command.Email,
            Roles.Student);

        return new AuthenticationResult(
            user,
            token);
    }
}