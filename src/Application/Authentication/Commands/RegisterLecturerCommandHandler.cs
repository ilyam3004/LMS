using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models;
using Domain.Common;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands;

public class RegisterLecturerCommandHandler(IJwtTokenGenerator _jwtTokenGenerator,
        IUnitOfWork _unitOfWork)
    : IRequestHandler<RegisterLecturerCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RegisterLecturerCommand command, 
        CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Users.UserExistsByEmail(command.Email))
        {
            return Errors.User.DuplicateEmail;
        }
        
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Email = command.Email,
            Password = command.Password
        };

        await _unitOfWork.Users.AddAsync(user);

        var lecturer = new Lecturer
        {
            LecturerId = Guid.NewGuid(),
            FullName = $"{command.FirstName} {command.LastName}",
            Degree = command.Degree,
            Address = command.Address,
            Birthday = command.Birthday.ToUniversalTime(),
            UserId = user.UserId
        };

        await _unitOfWork.Lecturers.AddAsync(lecturer);
        await _unitOfWork.SaveChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(
            user.UserId,
            command.FirstName,
            command.LastName,
            command.Email,
            "Lecturer");

        return new AuthenticationResult(
            user.UserId, 
            lecturer.FullName,
            user.Email,
            token);
    }
}

