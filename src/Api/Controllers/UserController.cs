using Application.Authentication.Commands;
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
        var command = new RegisterLecturerCommand(
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName,
            request.Degree,
            request.Birthday,
            request.Address);

        var result = await sender.Send(command);

        return result.Match<IActionResult>(
            value => Ok(mapper.Map<AuthenticationResponse>(value)),
            Problem);
    }
}