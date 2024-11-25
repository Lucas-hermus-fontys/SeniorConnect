using System;
using System.Data;
using Infrastructure.Exception;
using Infrastructure.Model;
using Infrastructure.Transformer;
using Infrastructure.Util;

namespace Infrastructure.Database.Repository;

public class UserRepository
{
    private readonly Database _databaseUtil;

    public UserRepository()
    {
        _databaseUtil = ServiceLocator.GetService<Database>();
    }

    public void CreateNewUser(User user)
    {
        _databaseUtil.ExecuteQuery(
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
        DataTable result = _databaseUtil.ExecuteQuery("Select * from user where email = ?;", email);
        if (result.Rows.Count == 0)
        {
            return null;
        }
        
        return UserTransformer.DataRowToUser(result.Rows[0]);
    }
}