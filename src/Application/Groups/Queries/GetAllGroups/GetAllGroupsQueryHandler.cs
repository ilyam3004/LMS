﻿using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Application.Models.Groups;
using MediatR;

namespace Application.Groups.Queries.GetAllGroups;

public class GetAllGroupsQueryHandler
    : IRequestHandler<GetAllGroupsQuery, Result<List<GroupResult>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllGroupsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<List<GroupResult>>> Handle(
        GetAllGroupsQuery request, 
        CancellationToken cancellationToken)
    {
        var groups = await _unitOfWork.Groups.GetAllGroupsWithStudents();
        
        return groups.Select(g => 
            new GroupResult(g, g.Students.Select(s => 
                new StudentResult(s)).ToList()))
            .ToList();
    }
}