using System.IdentityModel.Tokens.Jwt;
using Application.Common.Interfaces.Authentication;

namespace Infrastructure.Authentication;

public class JwtTokenReader : IJwtTokenReader
{
    public string? ReadUserIdFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

        var userId = jwtToken?.Claims
            .First(claim => 
                claim.Type == JwtRegisteredClaimNames.UniqueName).Value;

        return userId;
    }
}