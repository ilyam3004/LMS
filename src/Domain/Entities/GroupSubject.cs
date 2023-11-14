namespace Domain.Entities;

public class GroupSubject
{
    public Guid SubjectId { get; set; }
    public Guid GroupId { get; set; }
    public Subject Subject { get; set; } = null!;
    public Group Group { get; set; } = null!;
}