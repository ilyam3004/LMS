using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Features.Authentication.Queries.GetStudentProfile;
using Application.Models.Authentication;
using Domain.Abstractions.Results;
using Domain.Common;
using MediatR;

namespace Application.Authentication.Queries.GetStudentProfile;

public class GetStudentProfileQueryHandler
    : IRequestHandler<GetStudentProfileQuery, Result<ProfileResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public GetStudentProfileQueryHandler(IUnitOfWork unitOfWork, 
        IJwtTokenReader jwtTokenReader)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenReader;
    }

    public async Task<Result<ProfileResult>> Handle(GetStudentProfileQuery query,
        CancellationToken cancellationToken)
    {
        var userId = _jwtTokenReader.ReadUserIdFromToken(query.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await _unitOfWork.Users.GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        return new ProfileResult(user);
    }
}