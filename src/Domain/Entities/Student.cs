namespace Domain.Entities;

public class Student
{
    public Guid StudentId { get; set; }
    public Guid UserId { get; set; }
    public Guid GroupId { get; set; }
    public string FullName { get; set; } = null!;
    public DateTime Birthday { get; set; }
    public int Course { get; set; }
    public string Address { get; set; } = null!;
    public User User { get; set; } = null!;
    public Group Group { get; set; } = null!;
    public List<StudentTask> Tasks { get; set; } = null!;
}