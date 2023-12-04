using Application.Models.Authentication;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Authentication.Queries.GetLecturerProfile;

public record GetLecturerProfileQuery(
    string Token) : IRequest<Result<ProfileResult>>;