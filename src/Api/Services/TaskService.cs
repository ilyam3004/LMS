using Application.Features.Tasks.Queries.GetLecturerTaskDetails;
using Application.Features.Tasks.Queries.GetStudentTask;
using Application.Features.Tasks.Commands.UploadTaskSolution;
using Application.Features.Tasks.Commands.RemoveUploadedSolution;
using Application.Features.Tasks.Commands.AcceptTask;
using Application.Features.Tasks.Commands.ReturnTask;
using Application.Features.Tasks.Commands.RemoveTask;
using Application.Features.Tasks.Commands.CreateTask;
using Application.Features.Tasks.Commands.CreateComment;
using Microsoft.AspNetCore.Authorization;
using Task = Api.Protos.Task;
using Domain.Common;
using MapsterMapper;
using Api.Helpers;
using Api.Protos;
using Google.Protobuf.Collections;
using Grpc.Core;
using Mapster;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class TaskService : Task.TaskBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public TaskService(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    [Authorize(Roles = Roles.Lecturer)]
    public override async Task<LecturerSubjectResponse> AssignTask(AssignTaskRequest request,
        ServerCallContext context)
    {
        var command = _mapper.Map<AssignTaskCommand>(request);

        var result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<LecturerSubjectResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    [Authorize(Roles = Roles.Lecturer)]
    public override async Task<LecturerSubjectResponse> RemoveTask(RemoveTaskRequest request,
        ServerCallContext context)
    {
        var command = new RemoveTaskCommand(Guid.Parse(request.TaskId));

        var result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<LecturerSubjectResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    [Authorize(Roles = Roles.Lecturer)]
    public override async Task<LecturerTaskResponse> GetLecturerTaskDetails(GetLecturerTaskDetailsRequest request,
        ServerCallContext context)
    {
        var command = new GetLecturerTaskDetailsQuery(Guid.Parse(request.TaskId));

        var result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<LecturerTaskResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    [Authorize(Roles = Roles.Student)]
    public override async Task<StudentTaskResponse> GetStudentTaskDetails(GetStudentTaskDetailsRequest request,
        ServerCallContext context)
    {
        var token = context.GetHttpContext().Request.Headers.Authorization
            .ToString().Split(" ")[1];

        var command = new GetStudentTaskQuery(Guid.Parse(request.TaskId), token);

        var result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<StudentTaskResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    [Authorize(Roles = Roles.Student)]
    public override async Task<StudentTaskResponse> UploadTaskSolution(UploadTaskSolutionRequest request,
        ServerCallContext context)
    {
        var token = context.GetHttpContext().Request.Headers.Authorization
            .ToString().Split(" ")[1];

        var command = _mapper.Map<UploadTaskSolutionCommand>((request, token));

        var result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<StudentTaskResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    [Authorize(Roles = $"{Roles.Lecturer},{Roles.Student}")]
    public override Task<DownloadSolutionResponse> DownloadSolution(DownloadTaskSolutionRequest request,
        ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    [Authorize(Roles = Roles.Student)]
    public override async Task<StudentTaskResponse> RemoveTaskSolution(RemoveTaskSolutionRequest request,
        ServerCallContext context)
    {
        var token = context.GetHttpContext().Request.Headers.Authorization
            .ToString().Split(" ")[1];

        var command = new RemoveUploadedSolutionCommand(Guid.Parse(request.StudentTaskId),
            token);

        var result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<StudentTaskResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    public override async Task<LecturerTaskResponse> AcceptTask(AcceptTaskRequest request,
        ServerCallContext context)
    {
        var command = new AcceptTaskCommand(Guid.Parse(request.StudentTaskId), request.Grade);

        var result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<LecturerTaskResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    public override async Task<LecturerTaskResponse> ReturnTask(ReturnTaskRequest request,
        ServerCallContext context)
    {
        var command = new ReturnTaskCommand(Guid.Parse(request.StudentTaskId));

        var result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<LecturerTaskResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    public override async Task<LecturerTaskResponse> RejectTask(RejectTaskRequest request,
        ServerCallContext context)
    {
        var command = new ReturnTaskCommand(Guid.Parse(request.StudentTaskId));

        var result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<LecturerTaskResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }

    public override async Task<UploadedTaskResponse> CommentTask(CommentTaskRequest request, 
        ServerCallContext context)
    {
        var token = context.GetHttpContext().Request.Headers.Authorization
            .ToString().Split(" ")[1];

        var command = _mapper.Map<CommentTaskCommand>((request, token));

        var result = await _sender.Send(command);

        return result.Match(
            value => _mapper.Map<UploadedTaskResponse>(value),
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }
}