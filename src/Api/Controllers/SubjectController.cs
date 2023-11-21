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
public class SubjectController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public SubjectController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }
    
    [HttpPost]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> CreateSubject(CreateSubjectRequest request)
    {
        var token = Request.Headers.Authorization.ToString().Split(" ")[1];
        var command = _mapper.Map<CreateSubjectCommand>((request, token));

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<List<LecturerSubjectResponse>>(value)),
            Problem);
    }

    [HttpDelete("{subjectId}")]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> RemoveSubject(Guid subjectId)
    {
        var token = Request.Headers.Authorization.ToString().Split(" ")[1];
        var command = new RemoveSubjectCommand(subjectId, token);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<List<LecturerSubjectResponse>>(value)),
            Problem);
    }

    [HttpGet("lecturer")]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> GetLecturerSubjects()
    {
        var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
        var command = new GetLecturerSubjectsQuery(token);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<List<LecturerSubjectResponse>>(value)),
            Problem);
    }

    [HttpGet("student")]
    [Authorize(Roles = Roles.Student)]
    public async Task<IActionResult> GetStudentSubjects()
    {
        var token = Request.Headers["Authorization"].ToString().Split(" ")[1];
        var command = new GetStudentSubjectsQuery(token);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<List<StudentSubjectResponse>>(value)),
            Problem);
    }
}