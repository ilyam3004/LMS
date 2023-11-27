namespace Contracts.Responses.Tasks;

public record TaskCommentResponse(
    Guid TaskCommentId,
    string Comment,
    DateTime CreatedAt,
    string Username);