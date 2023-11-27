namespace Contracts.Responses.Tasks;

public record TaskCommentResponse(
    Guid Id,
    string Comment,
    DateTime CreatedAt,
    string Username);