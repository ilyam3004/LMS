using Contracts.Responses.Tasks;

namespace Contracts.Responses.Subjects;

public record StudentSubjectResponse(
    Guid SubjectId,
    string Name,
    string Description,
    string LecturerName,
    List<StudentTaskResponse> Tasks,
    double AverageGrade,
    int TotalGrade);