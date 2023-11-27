namespace Domain.Entities;

public class TaskComment
{
    public Guid TaskCommentId { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Guid StudentTaskId { get; set; }
    public Guid UserId { get; set; }
    public StudentTask StudentTask { get; set; } = null!;
    public User User { get; set; } = null!;
}