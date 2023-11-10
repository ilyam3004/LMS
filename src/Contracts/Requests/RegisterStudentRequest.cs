namespace Contracts.Requests;

public record RegisterStudentRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string GroupName,
    int Course,
    DateTime Birthday,
    string Address);