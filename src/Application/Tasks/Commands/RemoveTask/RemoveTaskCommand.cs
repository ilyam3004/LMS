using Domain.Abstractions.Results;
using Application.Models;
using MediatR;

namespace Application.Tasks.Commands.RemoveTask;

public record RemoveTaskCommand(
    Guid TaskId) : IRequest<Result<LecturerSubjectResult>>;