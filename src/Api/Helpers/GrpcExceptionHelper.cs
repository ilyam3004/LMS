using Domain.Abstractions.Errors;
using System.Text;
using Grpc.Core;

namespace Api.Helpers;

public static class GrpcExceptionHelper
{
    public static RpcException ConvertToRpcException(List<Error> errors)
    {
        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ConvertToValidationRpcException(errors);
        }

        return errors.Count is 0
            ? new RpcException(
                new Status(StatusCode.Unknown, "Something went wrong"))
            : MapErrorToRpcException(errors[0]);
    }

    private static RpcException MapErrorToRpcException(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCode.AlreadyExists,
            ErrorType.Validation => StatusCode.InvalidArgument,
            ErrorType.NotFound => StatusCode.NotFound,
            ErrorType.Unauthorized => StatusCode.Unauthenticated,
            _ => StatusCode.Unknown
        };

        return new RpcException(new Status(statusCode, error.Description));
    }

    private static RpcException ConvertToValidationRpcException(List<Error> errors)
    {
        StringBuilder builder = new();
        
        foreach (var error in errors)
            builder.Append($"{error.Description} ");
        
        var status = new Status(StatusCode.InvalidArgument, $"Validation failed: {builder}");

        var metadata = new Metadata();

        foreach (var error in errors)
            metadata.Add(new Metadata.Entry(error.Code, error.Description));

        return new RpcException(status, metadata);
    }
}