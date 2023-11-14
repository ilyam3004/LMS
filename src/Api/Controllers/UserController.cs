using Application.Authentication.Commands.RegisterLecturer;
using Application.Authentication.Commands.RegisterStudent;
using Application.Authentication.Queries.Login;
using Contracts.Requests.Authentication;
using Microsoft.AspNetCore.Mvc;
using Contracts.Responses;
using MapsterMapper;
using MediatR;

namespace Api.Controllers;

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

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = mapper.Map<LoginQuery>(request);

        var result = await sender.Send(query);

        return result.Match(
            value => Ok(mapper.Map<AuthenticationResponse>(value)),
            Problem);
    }
}