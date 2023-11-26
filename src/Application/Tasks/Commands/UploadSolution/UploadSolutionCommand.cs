using Application.Models;
using Domain.Abstractions.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Tasks.Commands.UploadSolution;

public record UploadSolutionCommand(
    IFormFile File, 
    Guid studentTaskId, 
    string Token) : IRequest<Result<StudentTaskResult>>;