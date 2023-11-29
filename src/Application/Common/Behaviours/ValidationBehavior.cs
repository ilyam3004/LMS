using Domain.Abstractions.Errors;
using Domain.Abstractions.Results;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviours;

public class ValidationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        if(_validator is null)
        {
            return await next(); 
        }

        var validationResult = await _validator.ValidateAsync(request, CancellationToken.None);
        if(validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
            .ConvertAll(validationFailure => Error.ValidationError(
                validationFailure.PropertyName, 
                validationFailure.ErrorMessage));

        return (dynamic)errors;
    }
}