using Domain.Entities;

namespace Application.Models;

public record AuthenticationResult(
    User User, 
    string Token);