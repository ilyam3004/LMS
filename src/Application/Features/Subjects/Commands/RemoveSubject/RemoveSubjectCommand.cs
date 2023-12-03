using Application.Models.Subjects;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Subjects.Commands.RemoveSubject;

public record RemoveSubjectCommand(
    Guid SubjectId,
    string Token) : IRequest<Result<List<LecturerSubjectResult>>>;