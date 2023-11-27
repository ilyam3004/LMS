using Microsoft.EntityFrameworkCore;
using Task = Domain.Entities.Task;
using Domain.Entities;

namespace Infrastructure.Persistence;

public class LmsDbContext : DbContext
{
    public LmsDbContext(DbContextOptions<LmsDbContext> options) : base(options)
    { }
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Lecturer> Lecturers { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<Subject> Subjects { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;
    public DbSet<StudentTask> StudentTasks { get; set; } = null!;
    public DbSet<TaskComment> TaskComments { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LmsDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}