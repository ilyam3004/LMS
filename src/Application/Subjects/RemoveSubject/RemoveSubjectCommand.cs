using Domain.Abstractions.Results;
using MediatR;

namespace Application.Subjects.RemoveSubject;

public record RemoveSubjectCommand(Guid SubjectId) 
    : IRequest<Result<string>>;