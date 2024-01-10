using Application.Features.Subjects.Queries.GetStudentSubjectsQuery;
using Application.Features.Subjects.Queries.GetLecturerSubjects;
using Application.Features.Subjects.Commands.RemoveSubject;
using Application.Features.Subjects.Commands.CreateSubject;
using Microsoft.AspNetCore.Authorization;
using Google.Protobuf.WellKnownTypes;
using Domain.Common;
using MapsterMapper;
using Api.Helpers;
using Api.Protos;
using Grpc.Core;
using MediatR;

namespace Api.Services;

public class SubjectService : Subject.SubjectBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public SubjectService(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [Authorize(Roles = Roles.Lecturer)]
    public override async Task<LecturerSubjectsResponse> CreateSubject(CreateSubjectRequest request,
        ServerCallContext context)
    {
        var token = context.GetHttpContext().Request.Headers.Authorization
            .ToString().Split(" ")[1];

        var command = _mapper.Map<CreateSubjectCommand>((request, token));

        var result = await _sender.Send(command);

        return result.Match(
            value => new LecturerSubjectsResponse
            {
                Subjects = {_mapper.Map<List<LecturerSubjectResponse>>(value)}
            },
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    [Authorize(Roles = Roles.Lecturer)]
    public override async Task<LecturerSubjectsResponse> GetLecturerSubjects(Empty request,
        ServerCallContext context)
    {
        var token = context.GetHttpContext().Request.Headers.Authorization
            .ToString().Split(" ")[1];

        var command = new GetLecturerSubjectsQuery(token);

        var result = await _sender.Send(command);

        return result.Match(
            value => new LecturerSubjectsResponse
            {
                Subjects = {_mapper.Map<List<LecturerSubjectResponse>>(value)}
            },
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    [Authorize(Roles = Roles.Student)]
    public override async Task<StudentSubjectsResponse> GetStudentSubjects(Empty request, 
        ServerCallContext context)
    {
        var token = context.GetHttpContext().Request.Headers.Authorization
            .ToString().Split(" ")[1];

        var command = new GetStudentSubjectsQuery(token);

        var result = await _sender.Send(command);

        return result.Match(
            value => new StudentSubjectsResponse
            {
                Subjects = {_mapper.Map<List<StudentSubjectResponse>>(value)}
            },
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    [Authorize(Roles = Roles.Lecturer)]
    public override async Task<LecturerSubjectsResponse> RemoveSubject(RemoveSubjectRequest request,
        ServerCallContext context)
    {
        var token = context.GetHttpContext().Request.Headers.Authorization
            .ToString().Split(" ")[1];

        var command = new RemoveSubjectCommand(Guid.Parse(request.SubjectId), token);

        var result = await _sender.Send(command);

        return result.Match(
            value => new LecturerSubjectsResponse
            {
                Subjects = {_mapper.Map<List<LecturerSubjectResponse>>(value)}
            },
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }
}