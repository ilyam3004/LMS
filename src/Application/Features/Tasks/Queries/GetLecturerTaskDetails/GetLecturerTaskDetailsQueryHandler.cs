﻿using Application.Common.Interfaces.Persistence;
using Application.Models.Tasks;
using Domain.Abstractions.Results;
using Domain.Common;
using MediatR;

namespace Application.Features.Tasks.Queries.GetLecturerTaskDetails;

public class GetLecturerTaskDetailsQueryHandler
    : IRequestHandler<GetLecturerTaskDetailsQuery, Result<LecturerTaskResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetLecturerTaskDetailsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<LecturerTaskResult>> Handle(GetLecturerTaskDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var task = await _unitOfWork.Tasks.GetTaskByIdWithRelations(query.TaskId);

        if (task is null)
            return Errors.Task.TaskNotFound;

        return new LecturerTaskResult(task);
    }
}