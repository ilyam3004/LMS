using Application.Models;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Tasks.Commands.CreateTask;

public record AssignTaskCommand(
    string Title,
    string Description, 
    Guid SubjectId,
    DateTime Deadline, 
    int MaxGrade,
    string Token) : IRequest<Result<LecturerSubjectResult>>;