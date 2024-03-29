﻿namespace Domain.Entities;

public class Group
{
    public Guid GroupId { get; set; }
    public string Name { get; set; } = null!;
    public int Course { get; set; }
    public string Department { get; set; } = null!;
    public List<Student> Students { get; set; } = null!;
    public List<Subject> Subjects { get; set; } = null!;
}