using Application.Models;
using Application.Models.Tasks;
using Domain.Abstractions.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Tasks.Commands.UploadSolution;

public record UploadTaskSolutionCommand(
    IFormFile File, 
    Guid studentTaskId, 
    string Token) : IRequest<Result<StudentTaskResult>>;