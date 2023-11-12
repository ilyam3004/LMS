using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Domain.Entities;
using Domain.Common;
using MediatR;

namespace Application.Authentication.Commands.RegisterLecturer;

public class RegisterLecturerCommandHandler(IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    : IRequestHandler<RegisterLecturerCommand, Result<AuthenticationResult>>
{
    public async Task<Result<AuthenticationResult>> Handle(
        RegisterLecturerCommand command, 
        CancellationToken cancellationToken)
    {
        if (await unitOfWork.Users.UserExistsByEmail(command.Email))
        {
            return Errors.User.DuplicateEmail;
        }
        
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Email = command.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(command.Password) 
        };

        await unitOfWork.Users.AddAsync(user);

        var lecturer = new Lecturer
        {
            LecturerId = Guid.NewGuid(),
            FullName = $"{command.FirstName} {command.LastName}",
            Degree = command.Degree,
            Address = command.Address,
            Birthday = command.Birthday.ToUniversalTime(),
            UserId = user.UserId
        };

        await unitOfWork.Lecturers.AddAsync(lecturer);
        await unitOfWork.SaveChangesAsync();

        var token = jwtTokenGenerator.GenerateToken(
            user.UserId,
            lecturer.FullName,
            user.Email,
            Roles.Lecturer);

        return new AuthenticationResult(
            user.UserId, 
            lecturer.FullName,
            user.Email,
            token);
    }
}