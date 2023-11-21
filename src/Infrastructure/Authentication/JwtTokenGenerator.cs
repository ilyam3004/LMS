using Application.Common.Interfaces.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Application.Services;
using System.Text;

namespace Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, 
        IOptions<JwtSettings> options)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = options.Value;
    }

    public string GenerateToken(Guid id, string fullName,
        string email, string role)
    { 
        var (firstName, surname) = SplitFullName(fullName);
        
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
            expires: _dateTimeProvider.UtcNow.AddHours(4),
            signingCredentials: signinCredentials);
        
       return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
    
    private (string, string) SplitFullName(string fullName)
    {
        string[] nameParts = fullName.Split(' ');

        return (nameParts[0], nameParts[1]);
    }
}