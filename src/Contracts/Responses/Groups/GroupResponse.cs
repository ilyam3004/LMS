using Contracts.Responses.Students;

namespace Contracts.Responses.Groups;

public record GroupResponse(
    Guid GroupId, 
    string Name, 
    string Department,
    List<StudentResponse> Students);