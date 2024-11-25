using Domain.Util;
using Infrastructure.Database.Repository;
using Infrastructure.Exception;
using Infrastructure.Model;

namespace Domain.Service;

public class AuthenticationService
{
    private UserRepository _userRepository = new UserRepository();

    public void RegisterNewUser(string email, string password)
    {
        if (_userRepository.GetByEmail(email) != null) { throw new EmailAlreadyExistsException(); }

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
        User user = _userRepository.GetByEmail(email);
        if (user == null) return false;
        return user.Password == TokenProviderUtil.GetSha256Hash(password + user.Salt);
    }
}