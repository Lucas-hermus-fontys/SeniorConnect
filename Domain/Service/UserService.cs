using Infrastructure.Database.Repository;
using Infrastructure.Exception;
using Infrastructure.Model;

namespace Domain.Service;

public class UserService
{
    private UserRepository userRepository = new();

    public User GetByEmail(String email)
    {
        try
        {
            return userRepository.GetByEmail(email);
        }
        catch (Exception ex)
        {
            throw new UserNotFoundException();
        }
    }
}