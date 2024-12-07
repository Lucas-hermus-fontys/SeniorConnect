using Domain.Exception;
using Domain.Interface;
using Domain.Model;

namespace Domain.Service;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public User GetByEmail(String email)
    {
        try
        {
            return _userRepository.GetByEmail(email);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
            throw new UserNotFoundException();
        }
    }
}