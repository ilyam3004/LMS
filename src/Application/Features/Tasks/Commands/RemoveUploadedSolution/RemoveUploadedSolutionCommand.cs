using Domain.Abstractions.Results;
using Application.Models.Tasks;
using MediatR;

namespace Application.Features.Tasks.Commands.RemoveUploadedSolution;

public record RemoveUploadedSolutionCommand(
    Guid StudentTaskId,
    string Token) : IRequest<Result<StudentTaskResult>>;