using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using taskManagement.Domain.Entities;
using TaskManagement.Application.Abstractions.Persistence;

namespace TaskManagement.Infrastructure.Services;

public class JwtService: IJwtService

{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(User user)
    {
        // Implementation for generating JWT token
        // This is a placeholder; actual implementation will depend on your JWT library and requirements

        var claims = new[]
        {
            new System.Security.Claims.Claim("id", user.Id.ToString()),
            new System.Security.Claims.Claim("name", user.Name),
            new System.Security.Claims.Claim("email", user.Email),
            new System.Security.Claims.Claim("role", user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!));
        var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(

            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
          expires: DateTime.UtcNow.AddMinutes(
            int.Parse(_configuration["JwtSettings:ExpirationInMinutes"])),
            signingCredentials: creds


            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
