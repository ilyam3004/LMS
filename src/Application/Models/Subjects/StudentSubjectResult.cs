using Domain.Entities;

namespace Application.Models;

public record StudentSubjectResult(
    Subject Subject, 
    List<StudentTaskResult> Tasks);