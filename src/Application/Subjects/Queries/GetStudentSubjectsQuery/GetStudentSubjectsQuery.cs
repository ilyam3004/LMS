using Domain.Abstractions.Results;
using Application.Models;
using MediatR;

namespace Application.Subjects.Queries.GetStudentSubjectsQuery;

public record GetStudentSubjectsQuery(string Token)
    : IRequest<Result<List<StudentSubjectResult>>>;