using Domain.Abstractions.Results;
using Application.Models;
using Application.Models.Subjects;
using MediatR;

namespace Application.Tasks.Commands.RemoveTask;

public record RemoveTaskCommand(
    Guid TaskId) : IRequest<Result<LecturerSubjectResult>>;