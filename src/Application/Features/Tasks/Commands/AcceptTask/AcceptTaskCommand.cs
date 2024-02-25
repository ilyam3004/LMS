using Domain.Abstractions.Results;
using Application.Models.Tasks;
using MediatR;

namespace Application.Features.Tasks.Commands.AcceptTask;

public record AcceptTaskCommand(Guid StudentTaskId, int Grade)
    : IRequest<Result<LecturerTaskResult>>;