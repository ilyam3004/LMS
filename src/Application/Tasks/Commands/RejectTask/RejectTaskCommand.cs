using Domain.Abstractions.Results;
using Application.Models;
using Application.Models.Tasks;
using MediatR;

namespace Application.Tasks.Commands.RejectTask;

public record RejectTaskCommand(Guid StudentTaskId)
    : IRequest<Result<LecturerTaskResult>>;