using System.Data;
using Domain.Model;

namespace Domain.Transformer;

public class UserTransformer
{
    public static User DataRowToUser(DataRow dataRow)
    {
        return new User
        {
            UserRole = new UserRole { Id = (int) dataRow["role_id"]},
            Email = (string) dataRow["email"],
            Salt = (string) dataRow["salt"],
            Password = (string) dataRow["password"],
            Id = (int) dataRow["id"]
        };
    }
}