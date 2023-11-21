using Application.Authentication.Commands.RegisterLecturer;
using Application.Authentication.Commands.RegisterStudent;
using Application.Authentication.Queries.Login;
using Contracts.Requests.Authentication;
using Microsoft.AspNetCore.Mvc;
using Contracts.Responses.Authentication;
using MapsterMapper;
using MediatR;

namespace Api.Controllers;

[Route("api/users")]
public class UserController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }
    
    [HttpPost("lecturers/register")]
    public async Task<IActionResult> Register(RegisterLecturerRequest request)
    {
        var command = _mapper.Map<RegisterLecturerCommand>(request);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<AuthenticationResponse>(value)),
            Problem);
    }

    [HttpPost("students/register")]
    public async Task<IActionResult> RegisterStudent(RegisterStudentRequest request)
    {
        var command = _mapper.Map<RegisterStudentCommand>(request);

        var result = await _sender.Send(command);

        return result.Match(
            value => Ok(_mapper.Map<AuthenticationResponse>(value)),
            Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var result = await _sender.Send(query);

        return result.Match(
            value => Ok(_mapper.Map<AuthenticationResponse>(value)),
            Problem);
    }
}