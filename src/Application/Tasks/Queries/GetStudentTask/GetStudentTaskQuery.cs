﻿using Domain.Abstractions.Results;
using Application.Models;
using Application.Models.Tasks;
using MediatR;

namespace Application.Tasks.Queries.GetStudentTask;

public record GetStudentTaskQuery(
    Guid TaskId, 
    string Token) : IRequest<Result<StudentTaskResult>>;