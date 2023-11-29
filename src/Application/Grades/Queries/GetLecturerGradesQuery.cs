using Application.Models.Grades;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Grades.Queries;

public record GetLecturerGradesQuery(
    string Token) : IRequest<Result<List<SubjectGradesResult>>>;