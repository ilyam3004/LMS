using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController : ApiController
{
    [HttpGet]
    [Authorize(Roles = "Lecturer")]
    public IActionResult GetAllTasks()
    {
        return Ok("Successfully Authorized");
    }
}