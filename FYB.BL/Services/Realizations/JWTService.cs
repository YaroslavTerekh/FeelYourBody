using FYB.BL.Services.Abstractions;
using FYB.Data.Entities;
using FYB.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FYB.BL.Services.Realizations;

public class JWTService : IJWTService
{
    private readonly IConfiguration _configuration;

    public JWTService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public JWTResponse GenerateJWT(User user)
    {
        List<Claim> claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.NameIdentifier, value: user.Id.ToString()),
                new Claim(type: ClaimTypes.Email, user.Email),
                new Claim(type: ClaimTypes.Role, user.Role.ToString())
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expireDate = DateTime.UtcNow.AddDays(14);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expireDate,
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return new JWTResponse
        {
            Token = jwt,
            ExpireDate = expireDate
        };
    }
}
