namespace Domain.Abstractions.Errors;

public class Error(ErrorType type, string code, string description)
{
    public ErrorType Type { get; } = type;
    public string Code { get; } = code;
    public string Description { get; } = description;

    public static Error ValidationError(string code, string description)
        => new(ErrorType.Validation, code, description);

    public static Error NotFound(string code, string description)
        => new(ErrorType.NotFound, code, description);
    
    public static Error Conflict(string code, string description)
        => new(ErrorType.Conflict, code, description);
    
    public static Error Unauthorized(string code, string description)
        => new(ErrorType.Unauthorized, code, description);
}