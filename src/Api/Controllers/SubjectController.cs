using Application.Subjects.Commands.CreateSubject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contracts.Requests.Subjects;
using Contracts.Responses.Subjects;
using Domain.Common;
using MapsterMapper;
using MediatR;

namespace Api.Controllers;

[Route("api/subjects")]
[Authorize(Roles = Roles.Lecturer)]
public class SubjectController(ISender sender, IMapper mapper) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateSubject(CreateSubjectRequest request)
    {
        string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
        var command = mapper.Map<CreateSubjectCommand>((request, token));

        var result = await sender.Send(command);

        return result.Match(
            value => Ok(mapper.Map<List<SubjectResponse>>(value)),
            Problem);
    }

    // [HttpDelete("{subjectId}")]
    // public async Task<IActionResult> RemoveSubject(Guid subjectId)
    // {
    //     var command = new RemoveSubjectCommand(subjectId);

    //     var result = await sender.Send(command);

    //     return result.Match(
    //         value => Ok(mapper.Map<>(value)),
    //         Problem);
    // }

    // [HttpGet]
    // public async Task<IActionResult> GetLecturerSubjects()
    // {
    //     string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
    //     var command = new GetLecturerSubjectsCommand(token);

    //     var result = await sender.Send(command);

    //     return result.Match(
    //         value => Ok(mapper.Map<>(value)),
    //         Problem);
    // }
}