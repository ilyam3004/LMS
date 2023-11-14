namespace Application.Common.Interfaces.Authentication;

public interface IJwtTokenReader
{
    string? ReadUserIdFromToken(string token);
}