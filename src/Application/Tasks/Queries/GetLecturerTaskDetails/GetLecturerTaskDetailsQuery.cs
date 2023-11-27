using Domain.Abstractions.Results;
using Application.Models;
using Application.Models.Tasks;
using MediatR;

namespace Application.Tasks.Queries.GetLecturerTaskDetails;

public record GetLecturerTaskDetailsQuery(
    Guid TaskId) : IRequest<Result<LecturerTaskResult>>;