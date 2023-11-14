using Domain.Abstractions.Results;
using MediatR;

namespace Application.Subjects.CreateSubject;

public record CreateSubjectCommand(
    string Name,
    string Description,
    string GroupName) : IRequest<Result<>>;