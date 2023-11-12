using Domain.Abstractions.Errors;

namespace Domain.Abstractions.Results;

public interface IResult
{
    bool IsSuccess { get; }
    List<Error> Errors { get; }
}