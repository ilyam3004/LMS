using Application.Models.Subjects;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Subjects.Queries.GetStudentSubjectsQuery;

public record GetStudentSubjectsQuery(string Token)
    : IRequest<Result<List<StudentSubjectResult>>>;