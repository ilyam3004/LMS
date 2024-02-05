namespace Domain.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Student? Student { get; set; }
    public Lecturer? Lecturer { get; set; }
}