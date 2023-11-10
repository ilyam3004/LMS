﻿namespace Contracts.Requests;

public record RegisterLecturerRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string? Degree,
    DateTime Birthday,
    string Address);