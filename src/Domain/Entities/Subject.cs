namespace Domain.Entities;

public class Subject
{
    public Guid SubjectId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Guid LecturerId { get; set; }
    public Guid GroupId { get; set; }
    public Lecturer Lecturer { get; set; } = null!;
    public Group Group { get; set; } = null!;
    public List<Task> Tasks { get; set; } = null!;
}