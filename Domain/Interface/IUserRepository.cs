using Domain.Model;

namespace Domain.Interface;

public interface IUserRepository
{
    public User GetByEmail(string email);
    public void CreateNewUser(User user);
}