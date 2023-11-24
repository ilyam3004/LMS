namespace Domain.Entities;

public class Task
{
    public Guid TaskId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid SubjectId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? Deadline { get; set; }
    public int MaxGrade { get; set; }
    public Subject Subject { get; set; } = null!;
}