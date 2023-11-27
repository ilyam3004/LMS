using Domain.Abstractions.Results;
using Application.Models;
using MediatR;

namespace Application.Tasks.Commands.RejectTask;

public record RejectTaskCommand(Guid StudentTaskId)
    : IRequest<Result<LecturerTaskResult>>;