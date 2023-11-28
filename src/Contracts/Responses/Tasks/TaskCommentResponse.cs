namespace Contracts.Responses.Tasks;

public record TaskCommentResponse(
    Guid TaskCommentId,
    Guid UserId,
    string Comment,
    DateTime CreatedAt,
    string Username);