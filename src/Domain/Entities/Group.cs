namespace Domain.Entities;

public class Group
{
    public Guid GroupId { get; set; }
    public string Name { get; set; } = null!;
    public string Department { get; set; } = null!;
    public List<Student>? Students { get; set; }
}