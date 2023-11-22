namespace Contracts.Requests.Tasks;

public record AssignTaskRequest(
    string Title, 
    string Description, 
    Guid SubjectId, 
    DateTime Deadline, 
    int MaxGrade);