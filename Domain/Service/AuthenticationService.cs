using Domain.Exception;
using Domain.Interface;
using Domain.Model;
using Domain.Util;

namespace Domain.Service;

public class AuthenticationService
{
    private IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

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