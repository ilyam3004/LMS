using Application.Tasks.Queries.GetLecturerTaskDetails;
using Application.Tasks.Commands.AcceptTask;
using Application.Tasks.Commands.CreateComment;
using Application.Tasks.Commands.CreateTask;
using Application.Tasks.Commands.RejectTask;
using Application.Tasks.Commands.RemoveTask;
using Application.Tasks.Commands.ReturnTask;
using Application.Tasks.Commands.UploadSolution;
using Application.Tasks.Queries.DownloadTaskSolution;
using Application.Tasks.Queries.GetStudentTask;
using Microsoft.AspNetCore.Authorization;
using Contracts.Requests.Tasks;
using Contracts.Responses.Subjects;
using Contracts.Responses.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Common;
using MapsterMapper;
using MediatR;

namespace Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public TaskController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> AssignTask(AssignTaskRequest request)
    {
        var command = _mapper.Map<AssignTaskCommand>(request);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<LecturerSubjectResponse>(value)),
            Problem);
    }

    [HttpDelete("{taskId}")]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> RemoveTask(Guid taskId)
    {
        var command = new RemoveTaskCommand(taskId);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<LecturerSubjectResponse>(value)),
            Problem);
    }

    [HttpGet("lecturer/{taskId}")]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> GetLecturerTaskDetails(Guid taskId)
    {
        var command = new GetLecturerTaskDetailsQuery(taskId);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<LecturerTaskResponse>(value)),
            Problem);
    }

    [HttpGet("student/{taskId}")]
    [Authorize(Roles = Roles.Student)]
    public async Task<IActionResult> GetStudentTask(Guid taskId)
    {
        var token = Request.Headers.Authorization.ToString().Split(" ")[1];
        var command = new GetStudentTaskQuery(taskId, token);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<StudentTaskResponse>(value)),
            Problem);
    }

    [HttpPut("{studentTaskId}/upload")]
    [Authorize(Roles = Roles.Student)]
    public async Task<IActionResult> UploadSolution([FromRoute] Guid studentTaskId,
        [FromForm] IFormFile file)
    {
        var token = Request.Headers.Authorization.ToString().Split(" ")[1];

        var command = new UploadTaskSolutionCommand(file, studentTaskId, token);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<StudentTaskResponse>(value)),
            Problem);
    }

    [HttpGet("{studentTaskId}/download")]
    [Authorize(Roles = $"{Roles.Lecturer},{Roles.Student}")]
    public async Task<IActionResult> DownloadSolution(Guid studentTaskId)
    {
        var command = new DownloadTaskSolutionQuery(studentTaskId);

        var result = await _sender.Send(command);

        return result.Match(
            value => 
                File(value.FileContent, value.ContentType, value.FileName),
            Problem);
    }

    [HttpPut("{studentTaskId}/return")]
    public async Task<IActionResult> ReturnTask(Guid studentTaskId)
    {
        var command = new ReturnTaskCommand(studentTaskId);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<LecturerTaskResponse>(value)),
            Problem);
    }

    [HttpPut("{studentTaskId}/accept")]
    public async Task<IActionResult> AcceptTask([FromRoute] Guid studentTaskId,
        [FromBody] AcceptTaskRequest request)
    {
        var command = new AcceptTaskCommand(studentTaskId, request.Grade);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<LecturerTaskResponse>(value)),
            Problem);
    }
    
    [HttpPut("{studentTaskId}/reject")]
    public async Task<IActionResult> RejectTask(Guid studentTaskId)
    {
        var command = new RejectTaskCommand(studentTaskId);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<LecturerTaskResponse>(value)),
            Problem);
    }

    [HttpPut("{studentTaskId}/comment")]
    public async Task<IActionResult> CreateComment([FromRoute] Guid studentTaskId,
        [FromBody] CreateCommentRequest request)
    {
        var token = Request.Headers.Authorization.ToString().Split(" ")[1];

        var command = new CreateCommentCommand(request.Comment, studentTaskId, token);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<StudentTaskResponse>(value)),
            Problem);
    }
}