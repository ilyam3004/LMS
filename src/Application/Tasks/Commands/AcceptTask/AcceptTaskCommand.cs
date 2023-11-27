using Application.Models;
using Application.Models.Tasks;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Tasks.Commands.AcceptTask;

public record AcceptTaskCommand(Guid StudentTaskId, int Grade) 
    : IRequest<Result<LecturerTaskResult>>;