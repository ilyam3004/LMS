using Application.Models.Subjects;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Subjects.Commands.CreateSubject;

public record CreateSubjectCommand(
    string Name,
    string Description,
    string GroupName,
    string Token) : IRequest<Result<List<LecturerSubjectResult>>>;