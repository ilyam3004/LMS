namespace Domain.Entities;

public class Lecturer
{
    public Guid LecturerId { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string? Degree { get; set; }
    public DateTime Birthday { get; set; }
    public string Address { get; set; } = null!;
    public User User { get; set; } = null!;
}