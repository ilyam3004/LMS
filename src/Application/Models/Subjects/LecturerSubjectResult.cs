using Application.Models.Tasks;
using Domain.Entities;

namespace Application.Models.Subjects;

public record LecturerSubjectResult(
    Subject Subject, 
    GroupResult Group,
    List<LecturerTaskResult> Tasks);