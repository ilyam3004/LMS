﻿using Application.Models.Subjects;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Features.Subjects.Queries.GetLecturerSubjects;

public record GetLecturerSubjectsQuery(string Token)
    : IRequest<Result<List<LecturerSubjectResult>>>;