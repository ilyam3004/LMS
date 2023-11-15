using Application.Models;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Subjects.Queries.GetLecturerSubjects;

public record GetLecturerSubjectsQuery(string Token)
    : IRequest<Result<List<LecturerSubjectResult>>>;