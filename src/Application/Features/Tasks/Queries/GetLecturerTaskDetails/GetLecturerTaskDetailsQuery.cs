using Application.Models.Tasks;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Tasks.Queries.GetLecturerTaskDetails;

public record GetLecturerTaskDetailsQuery(
    Guid TaskId) : IRequest<Result<LecturerTaskResult>>;