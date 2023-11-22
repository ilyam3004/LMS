using Application.Tasks.Commands;
using Application.Tasks.Commands.CreateTask;
using Application.Tasks.Commands.RemoveTask;
using Contracts.Requests.Tasks;
using Contracts.Responses.Subjects;
using Domain.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    [Authorize(Roles = Roles.Student)]
    public IActionResult GetAllTasks()
    {
        return Ok("Successfully Authorized");
    }

    [HttpPost("assign")]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> AssignTask(AssignTaskRequest request)
    {
        var token = Request.Headers.Authorization;
        var command = _mapper.Map<AssignTaskCommand>((request, token));
        
        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<LecturerSubjectResponse>(value)),
            Problem);
    }
    
    [HttpDelete("{taskId}")]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task <IActionResult> RemoveTask(Guid taskId)
    {
        var token = Request.Headers.Authorization;

        var command = new RemoveTaskCommand(taskId, token);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<LecturerSubjectResponse>(value)),
            Problem);
    }
}