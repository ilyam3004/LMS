using Application.Models;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Groups.Queries.GetAllGroups;

public class GetAllGroupsQuery() : IRequest<Result<List<GroupResult>>>;