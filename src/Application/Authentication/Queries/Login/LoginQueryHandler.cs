using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models;
using Domain.Abstractions.Results;
using Domain.Common;
using MediatR;

namespace Application.Authentication.Queries.Login;

public class LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork) 
    : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
{
    public async Task<Result<AuthenticationResult>> Handle(
        LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetUserByEmail(query.Email);

        if (user is null) 
        {
            return Errors.User.UserNotFound;
        }
        
        if (!BCrypt.Net.BCrypt.Verify(query.Password, user.Password))
        {
            return Errors.User.InvalidPassword;
        }
        
        var isStudent = user.Student is not null;

        if (isStudent)
        {
            var studentToken = jwtTokenGenerator.GenerateToken(
                user.UserId,
                user.Student!.FullName,
                user.Email,
                Roles.Student);

            return new AuthenticationResult(
                user,
                studentToken);
        }
        
        var lecturerToken = jwtTokenGenerator.GenerateToken(
            user.UserId,
            user.Lecturer!.FullName,
            user.Email,
            Roles.Lecturer);

        return new AuthenticationResult(
            user,
            lecturerToken);
    }
}