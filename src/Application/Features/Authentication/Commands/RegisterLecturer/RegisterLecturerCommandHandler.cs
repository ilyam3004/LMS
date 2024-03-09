using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Authentication;
using Domain.Abstractions.Results;
using Domain.Entities;
using Domain.Common;
using MediatR;

namespace Application.Features.Authentication.Commands.RegisterLecturer;

public class RegisterLecturerCommandHandler
    : IRequestHandler<RegisterLecturerCommand, Result<AuthenticationResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    public RegisterLecturerCommandHandler(IUnitOfWork unitOfWork, 
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<Result<AuthenticationResult>> Handle(RegisterLecturerCommand command, 
        CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Users.UserExistsByEmail(command.Email))
        {
            return Errors.Authentication.DuplicateEmail;
        }
        
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Email = command.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(command.Password) 
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

        await _unitOfWork.GetRepository<Lecturer>().AddAsync(lecturer);
        await _unitOfWork.SaveChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(
            user.UserId,
            lecturer.FullName,
            user.Email,
            Roles.Lecturer);

        return new AuthenticationResult(user, token);
    }
}