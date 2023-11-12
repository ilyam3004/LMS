using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController : ApiController
{
    [HttpGet]
    [Authorize(Roles = Roles.Student)]
    public IActionResult GetAllTasks()
    {
        return Ok("Successfully Authorized");
    }
}