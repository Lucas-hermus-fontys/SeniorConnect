using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Util;
using Infrastructure.Database.Repository;
using Infrastructure.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Service;

public class AuthenticationService
{
    private UserRepository _userRepository = new UserRepository();

    public void RegisterNewUser(string email, string password)
    {
        String salt = TokenProviderUtil.GenerateSalt();
        _userRepository.CreateNewUser(new User
        {
            UserRole = new UserRole { Id = 1, Name = "Admin" },
            Email = email,
            Salt = salt,
            Password = TokenProviderUtil.GetSha256Hash(password + salt)
        });
    }

    public bool LoginCredentialsAreValid(String email, String password)
    {
         DataRow user = _userRepository.GetByEmail(email);
         if (user == null) return false;
         return (string) user["password"] == TokenProviderUtil.GetSha256Hash(password + user["salt"]);
    }
}