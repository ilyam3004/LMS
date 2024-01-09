using Application.Features.Authentication.Commands.RegisterLecturer;
using Application.Authentication.Queries.GetLecturerProfile;
using Application.Authentication.Queries.GetStudentProfile;
using Application.Authentication.Commands.RegisterStudent;
using Application.Authentication.Queries.Login;
using Microsoft.AspNetCore.Authorization;
using Application.Models.Authentication;
using Domain.Abstractions.Results;
using Domain.Common;
using MapsterMapper;
using Api.Helpers;
using Api.Protos;
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

        Result<AuthenticationResult> result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<AuthenticationResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    public override async Task<AuthenticationResponse> RegisterStudent(RegisterStudentRequest request,
        ServerCallContext context)
    {
        var command = _mapper.Map<RegisterStudentCommand>(request);

        Result<AuthenticationResult> result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<AuthenticationResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    public override async Task<AuthenticationResponse> Login(LoginRequest request,
        ServerCallContext context)
    {
        var query = _mapper.Map<LoginQuery>(request);

        Result<AuthenticationResult> result = await _sender.Send(query);

        return result.Match(
            value => _mapper.Map<AuthenticationResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    [Authorize(Roles = Roles.Lecturer)]
    public override async Task<LecturerProfileResponse> LecturerProfile(LecturerProfileRequest request,
        ServerCallContext context)
    {
        string token = context.GetHttpContext().Request.Headers.Authorization
            .ToString().Split(" ")[1];

        GetLecturerProfileQuery query = new(token);

        Result<ProfileResult> result = await _sender.Send(query);

        return result.Match(
            value => _mapper.Map<LecturerProfileResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    [Authorize(Roles = Roles.Student)]
    public override async Task<StudentProfileResponse> StudentProfile(StudentProfileRequest request,
        ServerCallContext context)
    {
        string token = context.GetHttpContext().Request.Headers.Authorization
            .ToString().Split(" ")[1];

        GetStudentProfileQuery query = new(token);

        Result<ProfileResult> result = await _sender.Send(query);

        return result.Match(
            value => _mapper.Map<StudentProfileResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }
}