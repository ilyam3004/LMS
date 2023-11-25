using Application.Models;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Tasks.Commands.ReturnTask;

public record ReturnTaskCommand(Guid StudentTaskId)
    : IRequest<Result<TaskResult>>;