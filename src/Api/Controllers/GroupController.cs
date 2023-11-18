using Application.Groups.Queries.GetAllGroups;
using Contracts.Responses.Groups;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using MediatR;

namespace Api.Controllers;

[Route("api/groups")]
public class GroupController(ISender sender, IMapper mapper): ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllGroups()
    {
        var query = new GetAllGroupsQuery();

        var result = await sender.Send(query);

        return result.Match(
            value => Ok(mapper.Map<List<GroupResponse>>(value)),
            Problem);
    }
}