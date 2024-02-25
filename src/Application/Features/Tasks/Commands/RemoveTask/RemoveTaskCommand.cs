using Application.Models.Subjects;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Tasks.Commands.RemoveTask;

public record RemoveTaskCommand(
    Guid TaskId,
    string Token) : IRequest<Result<LecturerSubjectResult>>;