using Application.Models;
using Application.Models.Groups;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Groups.Queries.GetAllGroups;

public class GetAllGroupsQuery() : IRequest<Result<List<GroupResult>>>;