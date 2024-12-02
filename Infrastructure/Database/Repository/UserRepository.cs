using System.Data;
using Infrastructure.Interface;
using Infrastructure.Model;
using Infrastructure.Transformer;

namespace Infrastructure.Database.Repository;

public class UserRepository : IUserRepository
{
    private readonly IDatabase _database;

    public UserRepository(IDatabase database)
    {
        _database = database;
    }

    public void CreateNewUser(User user)
    {
        _database.ExecuteQuery(
            "INSERT INTO user (role_id, email, password, salt, active) VALUES (?, ?, ?, ?, ?);",
            user.UserRole.Id,
            user.Email,
            user.Password,
            user.Salt,
            1
        );
    }

    public User GetByEmail(string email)
    {
        DataTable result = _database.ExecuteQuery("Select * from user where email = ?;", email);
        if (result.Rows.Count == 0)
        {
            return null;
        }
        
        return UserTransformer.DataRowToUser(result.Rows[0]);
    }
}