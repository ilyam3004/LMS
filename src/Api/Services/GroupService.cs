using Application.Features.Groups.Queries.GetAllGroups;
using Domain.Abstractions.Results;
using Application.Models;
using MapsterMapper;
using Api.Helpers;
using Api.Protos;
using Application.Models.Groups;
using Grpc.Core;
using MediatR;

namespace Api.Services;

public class GroupService : Group.GroupBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GroupService(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override async Task<GetAllGroupsResponse> GetAllGroups(GetAllGroupsRequest request, 
        ServerCallContext context)
    {
        GetAllGroupsQuery query = new();
    
        Result<List<GroupResult>> result = await _sender.Send(query);
    
        return result.Match(
            value => new GetAllGroupsResponse
            {
                Groups = { _mapper.Map<List<GroupResponse>>(value) }
            },
            errors => throw GrpcExceptionHelper.ConvertToRpcException(errors));
    }
}