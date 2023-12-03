using Application.Models.Tasks;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Tasks.Commands.RejectTask;

public record RejectTaskCommand(Guid StudentTaskId)
    : IRequest<Result<LecturerTaskResult>>;