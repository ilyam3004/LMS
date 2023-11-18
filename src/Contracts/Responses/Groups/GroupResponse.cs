namespace Contracts.Responses.Groups;

public record GroupResponse(
    Guid GroupId, 
    string Name, 
    string Department);