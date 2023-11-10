using Application.Common.Interfaces.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Application.Services;
using System.Text;

namespace Infrastructure.Authentication;

public class JwtTokenGenerator(IDateTimeProvider dateTimeProvider, 
    IOptions<JwtSettings> options) : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings = options.Value;
    public string GenerateToken(Guid id, string firstName, string surname,
        string email, string role)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.UniqueName, id.ToString()),
            new(JwtRegisteredClaimNames.GivenName, firstName),
            new(JwtRegisteredClaimNames.FamilyName, surname),
            new(JwtRegisteredClaimNames.Email, email),
            new(CustomJwtClaimNames.Role, role)
        };

        var signinCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: dateTimeProvider.UtcNow.AddMinutes(30),
            signingCredentials: signinCredentials);
        
       return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}