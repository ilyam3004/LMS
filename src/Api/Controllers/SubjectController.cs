using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contracts.Requests.Subjects;
using Domain.Common;
using MapsterMapper;
using MediatR;

namespace Api.Controllers;

[Route("api/subjects")]
public class SubjectController(ISender sender, IMapper mapper) : ApiController
{
    [HttpPost]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> CreateSubject(CreateSubjectRequest request)
    {
        var command = mapper.Map<CreateSubjectCommand>(request);

        var result = await sender.Send(command);

        return result.Match(
            value => Ok(mapper.Map<>(value)),
            Problem);
    }

    [HttpDelete("{subjectId}")]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> RemoveSubject(int subjectId)
    {
        var command = new RemoveSubjectCommand(subjectId);

        var result = await sender.Send(command);

        return result.Match(
            value => Ok(mapper.Map<>(value)),
            Problem);
    }

    [HttpGet]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> GetLecturerSubjects()
    {
        string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
        var command = new GetLecturerSubjectsCommand(token);

        var result = await sender.Send(command);

        return result.Match(
            value => Ok(mapper.Map<>(value)),
            Problem);
    }
}