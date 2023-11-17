using Application.Subjects.Commands.CreateSubject;
using Application.Subjects.Commands.RemoveSubject;
using Application.Subjects.Queries.GetLecturerSubjects;
using Application.Subjects.Queries.GetStudentSubjectsQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contracts.Requests.Subjects;
using Contracts.Responses.Subjects;
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
        var token = Request.Headers.Authorization.ToString().Split(" ")[1];
        var command = mapper.Map<CreateSubjectCommand>((request, token));

        var result = await sender.Send(command);

        return result.Match(
            value => Ok(mapper.Map<List<LecturerSubjectResponse>>(value)),
            Problem);
    }

    [HttpDelete("{subjectId}")]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> RemoveSubject(Guid subjectId)
    {
        var token = Request.Headers.Authorization.ToString().Split(" ")[1];
        var command = new RemoveSubjectCommand(subjectId, token);

        var result = await sender.Send(command);

        return result.Match(
            value => Ok(mapper.Map<List<LecturerSubjectResponse>>(value)),
            Problem);
    }

    [HttpGet]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> GetLecturerSubjects()
    {
        string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
        var command = new GetLecturerSubjectsQuery(token);

        var result = await sender.Send(command);

        return result.Match(
            value => Ok(mapper.Map<List<LecturerSubjectResponse>>(value)),
            Problem);
    }

    [HttpGet("student")]
    [Authorize(Roles = Roles.Student)]
    public async Task<IActionResult> GetStudentSubjects()
    {
        string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
        var command = new GetStudentSubjectsQuery(token);

        var result = await sender.Send(command);

        return result.Match(
            value => Ok(mapper.Map<List<StudentSubjectResponse>>(value)),
            Problem);
    }
}