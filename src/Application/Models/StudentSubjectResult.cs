using Domain.Entities;

namespace Application.Models;

public record StudentSubjectResult(
    Subject Subject, string lecturerName);