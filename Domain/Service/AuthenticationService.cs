using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Util;
using Infrastructure.Database.Models;
using Infrastructure.Database.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Service;

public class AuthenticationService
{
    private UserRepository _userRepository = new UserRepository();
    private readonly IConfiguration _configuration;

    public AuthenticationService(IConfiguration configuration)
    {
        _configuration  =configuration;
    }

    public void RegisterNewUser(string email, string password)
    {
        // ToDo: transform to user object before creation
        string salt = TokenProviderUtil.GenerateSalt();
        _userRepository.CreateNewUser(1, email, TokenProviderUtil.GetSha256Hash(password + salt), salt);
    }

    public bool LoginCredentialsAreValid(String email, String password)
    {
         DataRow user = _userRepository.GetByEmail(email);
         if (user == null) return false;
         return (string) user["password"] == TokenProviderUtil.GetSha256Hash(password + user["salt"]);
    }
    
    public string GenerateToken(int userId, String email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if (_configuration["Jwt:Key"] == null) _configuration["Jwt:Key"] = "";
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]); 
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[] 
            { 
                new Claim("id", userId.ToString()), 
                new Claim(ClaimTypes.Name, email) 
            }),
            Expires = DateTime.UtcNow.AddSeconds(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"], 
            Audience = _configuration["Jwt:Audience"] 
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}