using Application.Models.Groups;
using Domain.Entities;

namespace Application.Models;

public record GroupResult(Group group, List<StudentResult> Students);