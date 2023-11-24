namespace Domain.Entities;

public class StudentTask
{
    public Guid StudentTaskId { get; set; }
    public Guid TaskId { get; set; }
    public string FileUrl { get; set; }
    public DateTime UploadedAt { get; set; }
    public int Grade { get; set; }
    public TaskStatus Status { get; set; }
    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;
}