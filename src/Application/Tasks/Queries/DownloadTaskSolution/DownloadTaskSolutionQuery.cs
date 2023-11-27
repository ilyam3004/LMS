using Application.Models;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Tasks.Queries.DownloadTaskSolution;

public record DownloadTaskSolutionQuery(
    Guid StudentTaskId) : IRequest<Result<DownloadTaskResult>>;