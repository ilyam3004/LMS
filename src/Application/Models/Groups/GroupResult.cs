using Domain.Entities;

namespace Application.Models.Groups;

public record GroupResult(Group Group, List<StudentResult> Students);