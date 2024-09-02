using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services;

public class JwtService
{
    private readonly string _secret;
    private readonly string _issuer;
    
    public JwtService(string secret, string issuer)
    {
        _secret = secret;
        _issuer = issuer;
    }

    public String GenerateToken(int userId, string role)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
        issuer:_issuer,
        claims: claims,
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: creds
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}