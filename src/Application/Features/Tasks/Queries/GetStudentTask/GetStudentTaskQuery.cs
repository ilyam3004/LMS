using Application.Models.Tasks;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Tasks.Queries.GetStudentTask;

public record GetStudentTaskQuery(
    Guid TaskId, 
    string Token) : IRequest<Result<StudentTaskResult>>;