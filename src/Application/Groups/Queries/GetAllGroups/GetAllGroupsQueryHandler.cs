using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using MediatR;

namespace Application.Groups.Queries.GetAllGroups;

public class GetAllGroupsQueryHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllGroupsQuery, Result<List<GroupResult>>>
{
    public async Task<Result<List<GroupResult>>> Handle(
        GetAllGroupsQuery request, 
        CancellationToken cancellationToken)
    {
        var groups = await unitOfWork.Groups.GetAll();
        
        return groups.Select(g => new GroupResult(g)).ToList();
    }
}