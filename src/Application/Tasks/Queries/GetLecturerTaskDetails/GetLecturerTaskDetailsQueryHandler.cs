using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Domain.Common;
using MediatR;

namespace Application.Tasks.Queries.GetLecturerTaskDetails;

public class GetLecturerTaskDetailsQueryHandler
    : IRequestHandler<GetLecturerTaskDetailsQuery, Result<TaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLecturerTaskDetailsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TaskResult>> Handle(GetLecturerTaskDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Tasks.GetTaskByIdWithRelations(query.TaskId);

        if (task is null)
            return Errors.Task.TaskNotFound;

        return new TaskResult(task);
    }
}