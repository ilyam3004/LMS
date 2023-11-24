using Domain.Entities;

namespace Application.Models;

public record LecturerSubjectResult(
    Subject Subject, 
    GroupResult Group,
    List<TaskResult> Tasks);