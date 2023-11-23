using Domain.Entities;

namespace Application.Models;

public record LecturerSubjectResult(
    Subject Subject, 
    List<GroupResult> Groups,
    List<TaskResult> Tasks);