using Application.Tasks.Commands.CreateTask;
using Application.Tasks.Commands.RemoveTask;
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
        var token = Request.Headers.Authorization;

        var command = new RemoveTaskCommand(taskId);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<LecturerSubjectResponse>(value)),
            Problem);
    }
    
    [HttpGet("{taskId}")]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> GetTask(Guid taskId)
    {
        var token = Request.Headers.Authorization;

        var command = new RemoveTaskCommand(taskId);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<TaskResponse>(value)),
            Problem);
    }
}