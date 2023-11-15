using Domain.Abstractions.Results;
using Application.Models;
using MediatR;

namespace Application.Subjects.Commands.CreateSubject;

public record CreateSubjectCommand(
    string Name,
    string Description,
    string GroupName,
    string Token) : IRequest<Result<List<LecturerSubjectResult>>>;