using Domain.Enums;

namespace Domain.Entities;

public class StudentTask
{
    public Guid StudentTaskId { get; set; }
    public Guid TaskId { get; set; }
    public Guid StudentId { get; set; }
    public string OrdinalFileName { get; set; }
    public string? FileUrl { get; set; }
    public DateTime? UploadedAt { get; set; }
    public int Grade { get; set; }
    public StudentTaskStatus Status { get; set; }
    public Student Student { get; set; } = null!;
    public Task Task { get; set; } = null!;
}