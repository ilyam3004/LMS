﻿using Application.Models.Authentication;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Authentication.Queries.GetStudentProfile;

public record GetStudentProfileQuery(
    string Token) : IRequest<Result<ProfileResult>>;