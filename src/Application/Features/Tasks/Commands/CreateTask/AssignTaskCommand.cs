using Application.Models.Subjects;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Tasks.Commands.CreateTask;

public record AssignTaskCommand(
    string Title,
    string Description, 
    Guid SubjectId,
    DateTime? Deadline, 
    int MaxGrade) : IRequest<Result<LecturerSubjectResult>>;