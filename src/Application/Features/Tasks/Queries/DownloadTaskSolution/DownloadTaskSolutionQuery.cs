using Application.Models.Tasks;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Tasks.Queries.DownloadTaskSolution;

public record DownloadTaskSolutionQuery(
    Guid StudentTaskId) : IRequest<Result<DownloadTaskResult>>;