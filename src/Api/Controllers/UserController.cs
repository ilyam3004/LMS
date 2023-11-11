using Application.Authentication.Commands.RegisterLecturer;
using Application.Authentication.Commands.RegisterStudent;
using Microsoft.AspNetCore.Mvc;
using Contracts.Requests;
using Contracts.Responses;
using MapsterMapper;
using MediatR;

namespace Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(ISender sender, IMapper mapper) : ApiController
{
    [HttpPost("lecturers/register")]
    public async Task<IActionResult> Register(RegisterLecturerRequest request)
    {
        var command = mapper.Map<RegisterLecturerCommand>(request);

        var result = await sender.Send(command);

        return result.Match(
            value => Ok(mapper.Map<AuthenticationResponse>(value)),
            Problem);
    }

    [HttpPost("students/register")]
    public async Task<IActionResult> RegisterStudent(RegisterStudentRequest request)
    {
        var command = mapper.Map<RegisterStudentCommand>(request);

        var result = await sender.Send(command);


        return result.Match(
            value => Ok(mapper.Map<AuthenticationResponse>(value)),
            Problem);
    }
}