namespace Contracts.Requests.Subjects;

public record CreateSubjectRequest(
    string Name,
    string Description,
    string GroupName);