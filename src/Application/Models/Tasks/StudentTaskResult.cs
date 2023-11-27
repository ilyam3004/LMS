using Domain.Entities;
using Task = Domain.Entities.Task;

namespace Application.Models.Tasks;

public record StudentTaskResult(
    Task Task, StudentTask UploadedTask);