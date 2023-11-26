using Domain.Entities;
using Task = Domain.Entities.Task;

namespace Application.Models;

public record StudentTaskResult(
    Task Task, StudentTask UploadedTask);