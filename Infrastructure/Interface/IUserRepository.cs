using Infrastructure.Model;

namespace Infrastructure.Interface;

public interface IUserRepository
{
    public User GetByEmail(string email);
    public void CreateNewUser(User user);
}