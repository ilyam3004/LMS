using Application.Models.Tasks;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Tasks.Commands.UploadTaskSolution;

public record UploadTaskSolutionCommand(
    string FileName,
    byte[]? FileContent, 
    Guid StudentTaskId,
    string Token) : IRequest<Result<StudentTaskResult>>;