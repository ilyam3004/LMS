using Domain.Abstractions.Results;
using Application.Models.Tasks;
using MediatR;

namespace Application.Tasks.Commands.CreateComment;

public record CreateCommentCommand(
    string Comment,
    Guid StudentTaskId,
    string Token) : IRequest<Result<StudentTaskResult>>;