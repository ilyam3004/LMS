using Application.Models.Tasks;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Tasks.Commands.CreateComment;

public record CommentTaskCommand(
    string Comment,
    Guid StudentTaskId,
    string Token) : IRequest<Result<UploadedTaskResult>>;