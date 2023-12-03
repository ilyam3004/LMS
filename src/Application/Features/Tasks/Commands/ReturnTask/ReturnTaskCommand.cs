using Application.Models.Tasks;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Tasks.Commands.ReturnTask;

public record ReturnTaskCommand(Guid StudentTaskId)
    : IRequest<Result<LecturerTaskResult>>;