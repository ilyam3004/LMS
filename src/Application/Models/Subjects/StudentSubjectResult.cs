using Application.Models.Tasks;
using Domain.Entities;

namespace Application.Models.Subjects;

public record StudentSubjectResult(
    Subject Subject, 
    List<StudentTaskResult> Tasks,
    double AverageGrade,
    int TotalGrade);