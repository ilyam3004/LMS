using Application.Models;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Subjects.Commands.GetLecturerSubjects;

public record GetLecturerSubjectsCommand(string Token)
    : IRequest<Result<List<SubjectResult>>>;