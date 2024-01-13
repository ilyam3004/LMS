using Application.Features.Tasks.Commands.RemoveUploadedSolution;
using Application.Features.Tasks.Queries.DownloadTaskSolution;
using Application.Features.Tasks.Commands.UploadTaskSolution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Common;
using MapsterMapper;
using Api.Protos;
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

    [HttpPut("{studentTaskId:guid}/upload")]
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

    [HttpGet("{studentTaskId:guid}/download")]
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
}