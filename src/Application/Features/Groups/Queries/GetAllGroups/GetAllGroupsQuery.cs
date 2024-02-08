using Domain.Abstractions.Results;
using Application.Models.Groups;
using MediatR;

namespace Application.Features.Groups.Queries.GetAllGroups;

public class GetAllGroupsQuery() : IRequest<Result<List<GroupResult>>>;