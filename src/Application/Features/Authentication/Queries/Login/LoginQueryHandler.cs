using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Authentication;
using Domain.Abstractions.Results;
using Domain.Common;
using MediatR;

namespace Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IUnitOfWork unitOfWork, 
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<AuthenticationResult>> Handle(
        LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetUserByEmail(query.Email);

        if (user is null)
            return Errors.User.UserNotFound;

        if (!BCrypt.Net.BCrypt.Verify(query.Password, user.Password))
            return Errors.User.InvalidCredentials;

        if (user.Student is not null)
        {
            var studentToken = _jwtTokenGenerator.GenerateToken(
                user.UserId,
                user.Student!.FullName,
                user.Email,
                Roles.Student);

            return new AuthenticationResult(
                user,
                studentToken);
        }

        if (user.Lecturer is null) 
            return Errors.User.UserNotFound;
        
        var lecturerToken = _jwtTokenGenerator.GenerateToken(
            user.UserId,
            user.Lecturer!.FullName,
            user.Email,
            Roles.Lecturer);

        return new AuthenticationResult(
            user,
            lecturerToken);
    }
}