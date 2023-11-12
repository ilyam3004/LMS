using Domain.Abstractions.Errors;

namespace Domain.Abstractions.Results;

public class Result<TValue> : IResult
{
    private readonly List<Error> _errors;
    private readonly TValue _value;
    
    public List<Error> Errors => !IsSuccess ? _errors : new List<Error>();
    public bool IsSuccess { get; }

    private Result(TValue value)
    {
        _errors = new List<Error>();
        IsSuccess = true;
        _value = value;
    }

    private Result(Error error)
    {
        _errors = new List<Error> {error};
        IsSuccess = false;
        _value = default!;
    }

    private Result(List<Error> errors)
    {
        _errors = errors;
        IsSuccess = false;
        _value = default!;
    }

    public static implicit operator Result<TValue>(TValue value)
        => new(value);

    public static implicit operator Result<TValue>(Error error)
        => new(error);

    public static implicit operator Result<TValue>(List<Error> errors)
        => new(errors);

    public TResult Match<TResult>(Func<TValue, TResult> onSuccess, 
        Func<List<Error>, TResult> onError)
        => IsSuccess ? onSuccess(_value) : onError(Errors);
}