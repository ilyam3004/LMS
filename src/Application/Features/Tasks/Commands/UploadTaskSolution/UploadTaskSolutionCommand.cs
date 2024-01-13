using Application.Models.Tasks;
using Domain.Abstractions.Results;
using Microsoft.AspNetCore.Http;
using MediatR;

namespace Application.Features.Tasks.Commands.UploadTaskSolution;

public record UploadTaskSolutionCommand(
    IFormFile File, 
    Guid StudentTaskId, 
    string Token) : IRequest<Result<StudentTaskResult>>;