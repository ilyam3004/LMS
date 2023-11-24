namespace Domain.Entities;

public class Group
{
    public Guid GroupId { get; set; }
    public string Name { get; set; } = null!;
    public string Department { get; set; } = null!;
    public List<Student> Students { get; set; } = null!;
    public List<Subject> Subjects { get; set; } = null!;
    public List<GroupSubject> GroupSubjects { get; set; } = null!;
}