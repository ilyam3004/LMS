using Application.Groups.Queries.GetAllGroups;
using Contracts.Responses.Groups;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using MediatR;

namespace Api.Controllers;

[Route("api/groups")]
public class GroupController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GroupController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllGroups()
    {
        var query = new GetAllGroupsQuery();

        var result = await _sender.Send(query);

        return result.Match(
            value => Ok(_mapper.Map<List<GroupResponse>>(value)),
            Problem);
    }
}