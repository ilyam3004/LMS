using Domain.Abstractions.Results;
using Application.Models;
using Application.Models.Subjects;
using MediatR;

namespace Application.Subjects.Commands.RemoveSubject;

public record RemoveSubjectCommand(
    Guid SubjectId,
    string Token) : IRequest<Result<List<LecturerSubjectResult>>>;