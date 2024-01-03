using Application.Features.Authentication.Commands.RegisterLecturer;
using Application.Authentication.Commands.RegisterStudent;
using Application.Authentication.Queries.Login;
using MapsterMapper;
using Api.Helpers;
using Grpc.Core;
using MediatR;

namespace Api.Services;

public class UserService : User.UserBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserService(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override async Task<AuthenticationResponse> RegisterLecturer(
        RegisterLecturerRequest request, ServerCallContext context)
    {
        var command = _mapper.Map<RegisterLecturerCommand>(request);
        
        var result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<AuthenticationResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    public override async Task<AuthenticationResponse> RegisterStudent(RegisterStudentRequest request,
        ServerCallContext context)
    {
        var command = _mapper.Map<RegisterStudentCommand>(request);

        var result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<AuthenticationResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    public override async Task<AuthenticationResponse> Login(LoginRequest request, 
        ServerCallContext context)
    {
        var query = _mapper.Map<LoginQuery>(request);
    
        var result = await _sender.Send(query);
    
        return result.Match(
            value => _mapper.Map<AuthenticationResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    } 
}