using Application.Grades.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contracts.Responses.Grades;
using Domain.Common;
using MapsterMapper;
using MediatR;

namespace Api.Controllers;

[Route("api/grades")]
public class GradesController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public GradesController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }
    
    [HttpGet]
    [Authorize(Roles = Roles.Lecturer)]
    public async Task<IActionResult> GetLecturerGrades()
    {
        var token = Request.Headers.Authorization.ToString().Split(" ")[1];
        var command = new GetLecturerGradesQuery(token);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<List<SubjectGradesResponse>>(value)),
            Problem);
    }
}